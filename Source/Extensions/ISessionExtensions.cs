using System.Text.Json;
using Source.Session;

namespace Source.Extensions;

public static class ISessionExtensions
{
    public static TValue GetJson<TValue>(this ISession source, string key) 
        where TValue : SessionBase 
            => JsonSerializer.Deserialize<TValue>(source.GetString(key)!)!;

    public static void SetJson<TValue>(this ISession source, string key, TValue value) 
        where TValue : SessionBase
            => source.SetString(key, JsonSerializer.Serialize(value));
}