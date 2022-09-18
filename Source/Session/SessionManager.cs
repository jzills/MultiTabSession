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
        _sessionLocker.Add(httpContextAccessor.HttpContext!.Session.Id);
        _httpContextAccessor = httpContextAccessor;
    }

    public TSessionValue? Current => _sessionService.Get(
        _httpContextAccessor.HttpContext!.Request.Headers[SessionHeader.Session]);

    public Guid Add(string sessionId, TSessionValue value)
    {
        _sessionValidator.ValidateKey(sessionId);
        _sessionValidator.ValidateValue(value);

        lock (_sessionLocker.Current)
        {
            _sessionService.Add(sessionId, value);
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
