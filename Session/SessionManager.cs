using System.Text.Json;

namespace MultiTabSession.Session;

public class SessionManager<TSessionState> : ISessionManager<TSessionState> where TSessionState : SessionBase
{
    private readonly string _current;

    private readonly IHttpContextAccessor _context;

    private ISession _session => _context?.HttpContext?.Session ?? 
        throw new BadHttpRequestException("No session configured.");

    public SessionManager(
        IHttpContextAccessor context,
        IConfiguration configuration
    )
    {
        _context = context;
        _current = configuration["Session:Current"];
    }

    public TSessionState? Current 
    {
        get
        {
            var sessionJson = _session.GetString(_current);
            return !string.IsNullOrEmpty(sessionJson) ? 
                JsonSerializer.Deserialize<TSessionState>(sessionJson) : 
                null;
        }
    }

    public Guid AddSession(string sessionId, TSessionState value)
    {
        if (!Guid.TryParse(sessionId, out var _windowTabId))
            throw new FormatException("Bad session state key format.");

        value.Initialize(sessionId);
        var sessionJson = JsonSerializer.Serialize<TSessionState>(value);

        _session.SetString(sessionId, sessionJson);
        _session.SetString(_current, sessionJson);
        return _windowTabId;
    }

    public Guid UpdateSession(string sessionId, TSessionState value)
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

    public TSessionState? GetSession(string sessionId)
    {
        if (!Guid.TryParse(sessionId, out var _))
            throw new FormatException("Bad session state key format.");

        return JsonSerializer.Deserialize<TSessionState>(
            _session.GetString(sessionId) ??
            throw new KeyNotFoundException("No session stored for that window tab identifier.")
        );
    }

    public IEnumerable<TSessionState> GetSessions(bool ignoreCurrent = true)
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

    public void RemoveSession(string sessionId) => _session.Remove(sessionId);

    public void SetCurrent(string sessionId) => 
        _session.SetString(_current, 
            _session.GetString(sessionId) ?? 
            throw new KeyNotFoundException("Unable to find the current session."));
}
