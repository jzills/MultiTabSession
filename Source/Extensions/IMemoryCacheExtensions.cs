using System.Reflection;
using Microsoft.Extensions.Caching.Memory;

namespace MultiTabSession.Extensions;

public static class IMemoryCacheExtensions
{
    public static IEnumerable<string> GetKeys(this IMemoryCache source)
    {
        var cacheEntriesCollectionProperty = typeof(MemoryCache)
            .GetProperty("EntriesCollection", 
                BindingFlags.NonPublic | BindingFlags.Instance
            );

        var cacheEntriesCollection = cacheEntriesCollectionProperty!.GetValue(source) as dynamic;
        var cacheKeys = new List<string>();

        foreach (var cacheItem in cacheEntriesCollection!)
        {
            var cacheItemKey = cacheItem.GetType().GetProperty("Key").GetValue(cacheItem, null);
            cacheKeys.Add(cacheItemKey);
        }

        return cacheKeys;
    }
}