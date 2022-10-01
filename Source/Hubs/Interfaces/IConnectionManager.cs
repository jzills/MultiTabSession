namespace Source.Hubs;

public interface IConnectionManager
{
    void Add(string connectionId, string clientSessionId);
    bool Remove(string connectionId, out string? clientSessionId);
    IEnumerable<string> ValuesFor(params string[] connectionIds);
    IEnumerable<string> AllValuesExcept(string connectionId);
}