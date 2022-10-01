using Source.Session;

namespace Source.Extensions;

public static class IHeaderDictionaryExtensions
{
    public static bool TryGetInitialSessionValue(
        this IHeaderDictionary source, 
        out string sessionId) =>
            source.TryGetFirstValue(
                SessionHeader.InitializeSession, 
                out sessionId
            );

    public static bool TryGetPreviousSessionValue(
        this IHeaderDictionary source, 
        out string sessionId) =>
            source.TryGetFirstValue(
                SessionHeader.FromPreviousSession, 
                out sessionId
            );

    public static bool TryGetSessionValue(
        this IHeaderDictionary source, 
        out string sessionId) =>
            source.TryGetFirstValue(
                SessionHeader.Session, 
                out sessionId
            );

    private static bool TryGetFirstValue(
        this IHeaderDictionary source, 
        string key, 
        out string firstValue
    )
    {
        if (source.TryGetValue(key, out var value))
        {
            firstValue = value.First();
            return true;
        }

        firstValue = string.Empty;
        return false;
    }
}