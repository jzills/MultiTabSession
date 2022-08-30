namespace MultiTabSession.Session;

public interface ISessionManager<TSessionValue> where TSessionValue : SessionBase
{
    TSessionValue? Current { get; }
    Guid Add(string sessionId, TSessionValue value);
    Guid Update(string sessionId, TSessionValue value);
    TSessionValue CopyFrom(string sessionId, string copyFromSessionId);
    TSessionValue? Get(string sessionId);
    IEnumerable<TSessionValue>? GetAll();
    void Remove(string sessionId);
}