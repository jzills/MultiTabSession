namespace MultiTabSession.Session;

public interface ISessionLocker
{
    object Current { get; }
    void Add(string sessionId);
    object? Get(string sessionId);
    IEnumerable<string> GetKeys();
}

public class SessionLocker : ISessionLocker, IDisposable
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private Dictionary<string, object> _lockers = new();
    public SessionLocker(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

    public object Current => Get(_httpContextAccessor.HttpContext!.Session.Id)!;

    public void Add(string sessionId)
    {
        if (!_lockers.ContainsKey(sessionId))
        {
            _lockers.Add(sessionId, new object());
        }
    }

    public object? Get(string sessionId) => 
        _lockers.TryGetValue(sessionId, out var locker) ?
            locker : null;

    public IEnumerable<string> GetKeys() => _lockers.Keys;

    public void Dispose()
    {
        
    }
}