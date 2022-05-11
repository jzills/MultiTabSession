using System.Text.Json;

namespace MultiTabSession;

public interface ISessionManager<TSessionState> where TSessionState : class
{
    TSessionState? Current { get; }

    Guid AddSession(string sessionId, TSessionState value);

    Guid UpdateSession(string sessionId, TSessionState value);

    TSessionState? GetSession(string sessionId);

    void RemoveSession(string sessionId);

    void SetCurrent(string sessionId);
}

public class SessionManager<TSessionState> : ISessionManager<TSessionState> where TSessionState : class
{
    private readonly IHttpContextAccessor _context;

    private ISession _session => _context?.HttpContext?.Session ?? 
        throw new BadHttpRequestException("No session configured.");

    public SessionManager(IHttpContextAccessor context) => _context = context;

    public TSessionState? Current 
    {
        get
        {
            var sessionJson = _session.GetString("_current");
            return !string.IsNullOrEmpty(sessionJson) ? 
                JsonSerializer.Deserialize<TSessionState>(sessionJson) : 
                null;
        }
    }

    public Guid AddSession(string sessionId, TSessionState value)
    {
        if (!Guid.TryParse(sessionId, out var _windowTabId))
            throw new FormatException("Bad session state key format.");

        var sessionJson = JsonSerializer.Serialize<TSessionState>(value);
        _session.SetString(sessionId, sessionJson);
        _session.SetString("_current", sessionJson);
        return _windowTabId;
    }

    public Guid UpdateSession(string sessionId, TSessionState value)
    {
        if (!Guid.TryParse(sessionId, out var _windowTabId))
            throw new FormatException("Bad session state key format.");

        var sessionJson = JsonSerializer.Serialize<TSessionState>(value);

        if (!string.IsNullOrEmpty(_session.GetString(sessionId)))
        {
            _session.SetString(sessionId, sessionJson);
            _session.SetString("_current", sessionJson);
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
            throw new Exception("No session stored for that window tab identifier.")
        );
    }

    public void RemoveSession(string sessionId) => _session.Remove(sessionId);

    public void SetCurrent(string sessionId) => 
        _session.SetString("_current", _session.GetString(sessionId) ?? string.Empty);
}
