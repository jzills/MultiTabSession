using MultiTabSession.Extensions;

namespace MultiTabSession.Session;

public class SessionManagerV2
{
    private readonly ISessionService<SessionTab> _sessionService;
    private readonly ISessionValidator<SessionTab> _sessionValidator;
    private readonly ISessionLocker _sessionLocker;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionManagerV2(
        ISessionService<SessionTab> sessionService,
        ISessionValidator<SessionTab> sessionValidator,
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

    public SessionTab? Current => _httpContextAccessor.HttpContext!.Request.Headers
        .TryGetSessionHeader(SessionHeader.Session, out var sessionId) ?
            _sessionService.Get(sessionId!) : null;

    public Guid Add(string sessionId, SessionTab value)
    {
        _sessionValidator.ValidateKey(sessionId);
        _sessionValidator.ValidateValue(value);

        lock (_sessionLocker.Current)
        {
            _sessionService.Add(sessionId, value);
        }

        return Guid.Parse(sessionId);
    }

    public Guid Update(string sessionId, SessionTab value)
    {
        _sessionValidator.ValidateKey(sessionId);
        _sessionValidator.ValidateValue(value);

        lock (_sessionLocker.Current)
        {
            _sessionService.Update(sessionId, value);
        }
        
        return Guid.Parse(sessionId);
    }

    public SessionTab CopyFrom(string sessionId)
    {
        _sessionValidator.ValidateKey(sessionId);

        lock (_sessionLocker.Current)
        {
            var sessionTab = _sessionService.Get(sessionId);
            _sessionService.Add(sessionId, sessionTab!);
            return sessionTab!;
        }
    }

    public IEnumerable<SessionTab> Get(bool ignoreCurrent = true) => 
        _sessionService.Get(ignoreCurrent);

    public void Remove(string sessionId)
    {
        _sessionValidator.ValidateKey(sessionId);
        
        lock (_sessionLocker.Current)
        {
            _sessionService.Remove(sessionId);
        }
    }
}
