namespace MultiTabSession.Session;

public interface ISessionLocker
{
    object Current { get; }
    void Add(string sessionId);
    object? Get(string sessionId);
    IEnumerable<string> GetKeys();
}