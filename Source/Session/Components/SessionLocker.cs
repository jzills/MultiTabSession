using Microsoft.Extensions.Caching.Memory;
using Source.Extensions;

namespace Source.Session;

public class SessionLocker : ISessionLocker
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMemoryCache _memoryCache;

    public SessionLocker(
        IHttpContextAccessor httpContextAccessor,
        IMemoryCache memoryCache
    ) =>
        (_httpContextAccessor, _memoryCache) = 
            (httpContextAccessor, memoryCache);

    public object Current => Get(_httpContextAccessor.HttpContext!.Session.Id)!;

    public void Add(string sessionId)
    {
        if (!_memoryCache.TryGetValue(sessionId, out var _))
        {
            _memoryCache.Set(sessionId, new object(), new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(10)
            });
        }
    }

    public object? Get(string sessionId) => _memoryCache.Get(sessionId);

    public IEnumerable<string> GetKeys() => _memoryCache.GetKeys();
}