using System.Text.Json;
using MultiTabSession.Extensions;

namespace MultiTabSession.Session;

public class SessionManager<TSessionState> : ISessionManager<TSessionState> where TSessionState : SessionBase
{
    private static object _locker = new object();
    private static readonly string _current = Guid.NewGuid().ToString();
    private readonly IHttpContextAccessor _context;
    private readonly ISessionLocker _sessionLocker;

    private ISession _session => _context?.HttpContext?.Session ?? 
        throw new BadHttpRequestException("No session configured.");

    public SessionManager(IHttpContextAccessor context, ISessionLocker sessionLocker) => (_context, _sessionLocker) = (context, sessionLocker);

    public TSessionState? Current 
    {
        get
        {
            lock (_locker)
            {
                if (_context.HttpContext.Request.Headers
                    .TryGetSessionHeader(SessionHeader.Session, out var sessionId))
                {
                    return Get(sessionId);
                }

                return null;
            }
        }
    }

    public Guid Add(string sessionId, TSessionState value)
    {
        if (!Guid.TryParse(sessionId, out var _windowTabId))
            throw new FormatException("Bad session state key format.");

        value.Initialize(sessionId);
        var sessionJson = JsonSerializer.Serialize<TSessionState>(value);

        _session.SetString(sessionId, sessionJson);
        _session.SetString(_current, sessionJson);
        return _windowTabId;
    }

    public Guid Update(string sessionId, TSessionState value)
    {
        if (!Guid.TryParse(sessionId, out var _windowTabId))
            throw new FormatException("Bad session state key format.");

        value.ModifiedAt = DateTime.Now;
        var sessionJson = JsonSerializer.Serialize<TSessionState>(value);

        if (!string.IsNullOrEmpty(_session.GetString(sessionId)))
        {
            _session.SetString(sessionId, sessionJson);
            _session.SetString(_current, sessionJson);
        }
        else throw new KeyNotFoundException("Session does not exist.");
        
        return _windowTabId;
    }

    public TSessionState? Get(string sessionId)
    {
        if (!Guid.TryParse(sessionId, out var _))
            throw new FormatException("Bad session state key format.");

        return JsonSerializer.Deserialize<TSessionState>(
            _session.GetString(sessionId) ??
            throw new KeyNotFoundException("No session stored for that window tab identifier.")
        );
    }

    public IEnumerable<TSessionState> Get(bool ignoreCurrent = true)
    {
        var sessionStates = new List<TSessionState>();
        var sessionKeys = ignoreCurrent ?
            _session.Keys.Where(key => key != _current) : 
            _session.Keys;

        foreach (var key in sessionKeys)
        {
            var value = _session.GetString(key);
            if (!string.IsNullOrEmpty(value))
            {
                var sessionState = JsonSerializer.Deserialize<TSessionState>(value);
                if (sessionState != null)
                    sessionStates.Add(sessionState);
            }
        }

        return sessionStates;
    }

    public void Remove(string sessionId) => _session.Remove(sessionId);

    public void SetCurrent(string sessionId) => 
        _session.SetString(_current, 
            _session.GetString(sessionId) ?? 
            throw new KeyNotFoundException("Unable to find the current session."));
}
