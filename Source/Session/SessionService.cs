using System.Text.Json;
using MultiTabSession.Extensions;

namespace MultiTabSession.Session;

public interface ISessionService<TSessionValue>
{
    Guid Add(string sessionId, TSessionValue value);

    Guid Update(string sessionId, TSessionValue value);

    TSessionValue? Get(string sessionId);

    IEnumerable<TSessionValue> Get(bool ignoreCurrent = true);

    void Remove(string sessionId);
}

public class SessionService<TSessionValue> : ISessionService<TSessionValue> where TSessionValue : SessionBase
{
    private readonly ISession _session;

    public SessionService(IHttpContextAccessor httpContextAccessor) =>
        _session = httpContextAccessor.HttpContext!.Session;

    public Guid Add(string sessionId, TSessionValue value)
    {
        value.Initialize(sessionId);
        _session.SetJson(sessionId, value);
        // var sessionJson = JsonSerializer.Serialize(value);
        // _session.SetString(sessionId, sessionJson);
        return Guid.Parse(sessionId);
        //return _windowTabId;
    }

    public Guid Update(string sessionId, TSessionValue value)
    {
        value.ModifiedAt = DateTime.Now;
        _session.SetJson(sessionId, value);
        // var sessionJson = JsonSerializer.Serialize(value);
        // _session.SetString(sessionId, sessionJson);
        return Guid.Parse(sessionId);
    }

    public TSessionValue? Get(string sessionId) => 
        _session.GetJson<TSessionValue>(sessionId);

    public IEnumerable<TSessionValue> Get(bool ignoreCurrent = true)
    {
        var sessionStates = new List<TSessionValue>();
        // var sessionKeys = ignoreCurrent ?
        //     _session.Keys.Where(key => key != _current) : 
        //     _session.Keys;
        var sessionKeys = _session.Keys;

        foreach (var key in sessionKeys)
        {
            var value = _session.GetJson<TSessionValue>(key);
            sessionStates.Add(value);
        }

        return sessionStates;
    }

    public void Remove(string sessionId) => _session.Remove(sessionId);
}
