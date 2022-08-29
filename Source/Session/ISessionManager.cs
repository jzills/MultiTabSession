namespace MultiTabSession.Session;

public interface ISessionManager<TSessionValue> where TSessionValue : SessionBase
{
    TSessionValue? Current { get; }

    Guid Add(string sessionId, TSessionValue value);

    Guid Update(string sessionId, TSessionValue value);

    TSessionValue? Get(string sessionId);

    IEnumerable<TSessionValue>? Get(bool ignoreCurrent = true);

    void Remove(string sessionId);
}