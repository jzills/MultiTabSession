using MultiTabSession.Extensions;

namespace MultiTabSession.Session;

public class SessionService<TSessionValue> : ISessionService<TSessionValue> 
    where TSessionValue : SessionBase
{
    private readonly ISession _session;

    public SessionService(IHttpContextAccessor httpContextAccessor) =>
        _session = httpContextAccessor.HttpContext!.Session;

    public Guid Add(string sessionId, TSessionValue value)
    {
        value.Id = DateTime.Now.Ticks;
        value.WindowName = Guid.Parse(sessionId);
        value.CreatedAt = DateTime.Now;
        value.ModifiedAt = value.CreatedAt;

        _session.SetJson(sessionId, value);
        return Guid.Parse(sessionId);
    }

    public Guid Update(string sessionId, TSessionValue value)
    {
        value.ModifiedAt = DateTime.Now;
        
        _session.SetJson(sessionId, value);
        return Guid.Parse(sessionId);
    }

    public TSessionValue Get(string sessionId) => 
        _session.GetJson<TSessionValue>(sessionId);

    public IEnumerable<TSessionValue> GetAll()
    {
        var sessionValues = new List<TSessionValue>();
        foreach (var key in _session.Keys)
        {
            var value = _session.GetJson<TSessionValue>(key);
            sessionValues.Add(value);
        }

        return sessionValues;
    }

    public void Remove(string sessionId) => _session.Remove(sessionId);
}
