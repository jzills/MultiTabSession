using Source.Session;

namespace Source.Extensions;

public static class IHeaderDictionaryExtensions
{
    public static bool TryGetSessionHeader(
        this IHeaderDictionary source, 
        SessionHeader header, 
        out string sessionId
    )
    {
        if (source.TryGetValue(header, out var value))
        {
            sessionId = value.First();
            return true;
        }

        sessionId = string.Empty;
        return false;
    }
}