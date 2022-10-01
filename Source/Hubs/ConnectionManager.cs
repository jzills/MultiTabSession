namespace Source.Hubs;

public class ConnectionManager : IConnectionManager
{
    private readonly Dictionary<string, string> _connections = new();

    public void Add(string connectionId, string clientSessionId) =>
        _connections[connectionId] = clientSessionId;

    public bool Remove(string connectionId, out string? clientSessionId) =>
        _connections.Remove(connectionId, out clientSessionId);

    public IEnumerable<string> ValuesFor(params string[] connectionIds) =>
        _connections.Where(connection => connectionIds.Contains(connection.Key))
            .Select(connection => connection.Value);

    public IEnumerable<string> AllValuesExcept(string connectionId) =>
        _connections.Where(connection => connection.Key != connectionId)
            .Select(connection => connection.Value);
}