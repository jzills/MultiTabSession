using System.Text.Json;

namespace MultiTabSession.Extensions;

public static class ISessionExtensions
{
    public static TValue GetJson<TValue>(this ISession source, string key) =>
        JsonSerializer.Deserialize<TValue>(source.GetString(key)!)!;

    public static void SetJson<TValue>(this ISession source, string key, TValue value) =>
        source.SetString(key, JsonSerializer.Serialize(value));
}