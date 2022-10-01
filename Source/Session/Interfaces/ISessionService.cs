namespace Source.Session;

public interface ISessionService<TSessionValue>
{
    Guid Add(string sessionId, TSessionValue value);
    Guid Update(string sessionId, TSessionValue value);
    TSessionValue? Get(string sessionId);
    IEnumerable<TSessionValue> GetAll();
    void Remove(string sessionId);
}