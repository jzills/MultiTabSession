using System.Text.Json;
using MultiTabSession.Extensions;

namespace MultiTabSession.Session;

public class SessionManager<TSessionValue> : ISessionManager<TSessionValue> where TSessionValue : SessionBase
{
    private readonly IHttpContextAccessor _context;
    private readonly ISessionLocker _sessionLocker;

    private ISession _session => _context?.HttpContext?.Session ?? 
        throw new BadHttpRequestException("No session configured.");

    public SessionManager(IHttpContextAccessor context, ISessionLocker sessionLocker)
    {
        _context = context;
        _sessionLocker = sessionLocker;
        _sessionLocker.Add(context.HttpContext!.Session.Id);
    }

    public TSessionValue? Current => _context.HttpContext!.Request.Headers
        .TryGetSessionHeader(SessionHeader.Session, out var sessionId) ?
            Get(sessionId!) : null;

    public Guid Add(string sessionId, TSessionValue value)
    {
        if (!Guid.TryParse(sessionId, out var _windowTabId))
            throw new FormatException("Bad session state key format.");

        lock (_sessionLocker.Current)
        {
            value.Initialize(sessionId);
            var sessionJson = JsonSerializer.Serialize(value);
            _session.SetString(sessionId, sessionJson);
        }

        return _windowTabId;
    }

    public Guid Update(string sessionId, TSessionValue value)
    {
        if (!Guid.TryParse(sessionId, out var _windowTabId))
            throw new FormatException("Bad session state key format.");

        if (!string.IsNullOrEmpty(_session.GetString(sessionId)))
        {
            lock (_sessionLocker.Current)
            {
                value.ModifiedAt = DateTime.Now;
                var sessionJson = JsonSerializer.Serialize(value);
                _session.SetString(sessionId, sessionJson);
            }
        }
        else throw new KeyNotFoundException("Session does not exist.");
        
        return _windowTabId;
    }

    public TSessionValue? Get(string sessionId) => 
        JsonSerializer.Deserialize<TSessionValue>(
            _session.GetString(sessionId)!);
    // {
    //     if (!Guid.TryParse(sessionId, out var _))
    //         throw new FormatException("Bad session state key format.");

    //     return JsonSerializer.Deserialize<TSessionValue>(
    //         _session.GetString(sessionId) ??
    //         throw new KeyNotFoundException("No session stored for that window tab identifier.")
    //     );
    // }

    public IEnumerable<TSessionValue> Get(bool ignoreCurrent = true)
    {
        var sessionStates = new List<TSessionValue>();
        // var sessionKeys = ignoreCurrent ?
        //     _session.Keys.Where(key => key != _current) : 
        //     _session.Keys;
        var sessionKeys = _session.Keys;

        foreach (var key in sessionKeys)
        {
            var value = _session.GetString(key);
            if (!string.IsNullOrEmpty(value))
            {
                var sessionState = JsonSerializer.Deserialize<TSessionValue>(value);
                if (sessionState != null)
                    sessionStates.Add(sessionState);
            }
        }

        return sessionStates;
    }

    public void Remove(string sessionId)
    {
        lock (_sessionLocker.Current)
        {
            _session.Remove(sessionId);
        }
    }
}
