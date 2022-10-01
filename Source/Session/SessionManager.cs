namespace Source.Session;

public class SessionManager<TSessionValue> : ISessionManager<TSessionValue> 
    where TSessionValue : SessionBase
{ 
    private readonly ISessionService<TSessionValue> _sessionService;
    private readonly ISessionValidator<TSessionValue> _sessionValidator;
    private readonly ISessionLocker _sessionLocker;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionManager(
        ISessionService<TSessionValue> sessionService,
        ISessionValidator<TSessionValue> sessionValidator,
        ISessionLocker sessionLocker,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _sessionService = sessionService;
        _sessionValidator = sessionValidator;
        _sessionLocker = sessionLocker;
        _httpContextAccessor = httpContextAccessor;
    }

    public TSessionValue? Current
    {
        get 
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                if (httpContext.Request.Headers
                        .TryGetValue(SessionHeader.Session, out var sessionValue))
                {
                    var sessionId = sessionValue.First();
                    if (!string.IsNullOrEmpty(sessionId))
                    {
                        return Get(sessionId);
                    }
                }
            }

            return default(TSessionValue?);
        }
    }

    public Guid Add(string sessionId, TSessionValue value)
    {
        _sessionValidator.ValidateKey(sessionId);
        _sessionValidator.ValidateValue(value);

        lock (_sessionLocker.Current)
        {
            _sessionService.Add(sessionId, value);

            // TODO: Continue work on client session expiration
            var sessions = _sessionService.GetAll();
            var expiredSessions = sessions.Where(session => session.IsExpired()); 
            if (expiredSessions.Any()) 
            {
                foreach (var expiredSession in expiredSessions)
                {
                    _sessionService.Remove(expiredSession.ClientSessionId.ToString());
                }
            }
        }

        return Guid.Parse(sessionId);
    }

    public Guid Update(string sessionId, TSessionValue value)
    {
        _sessionValidator.ValidateKey(sessionId);
        _sessionValidator.ValidateValue(value);

        lock (_sessionLocker.Current)
        {
            _sessionService.Update(sessionId, value);
        }
        
        return Guid.Parse(sessionId);
    }

    public TSessionValue CopyFrom(string sessionId, string copyFromSessionId)
    {
        _sessionValidator.ValidateKey(sessionId);
        _sessionValidator.ValidateKey(copyFromSessionId);

        lock (_sessionLocker.Current)
        {
            var sessionTab = _sessionService.Get(copyFromSessionId);
            _sessionService.Add(sessionId, sessionTab!);
            return sessionTab!;
        }
    }

    public IEnumerable<TSessionValue> GetAll() => _sessionService.GetAll();

    public TSessionValue? Get(string sessionId)
    {
        _sessionValidator.ValidateKey(sessionId);
        return _sessionService.Get(sessionId);
    }

    public void Remove(string sessionId)
    {
        _sessionValidator.ValidateKey(sessionId);
        
        lock (_sessionLocker.Current)
        {
            _sessionService.Remove(sessionId);
        }
    }
}
