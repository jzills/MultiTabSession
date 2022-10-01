using Source.Session;

namespace Source.Extensions;

public static class IQueryCollectionExtensions
{
    public static bool TryGetSessionValue(
        this IQueryCollection source, 
        out string sessionId) =>
            source.TryGetFirstValue(
                SessionHeader.Session, 
                out sessionId
            );

    private static bool TryGetFirstValue(
        this IQueryCollection source, 
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