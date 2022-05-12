using MultiTabSession.Session;

namespace MultiTabSession.Extensions;

public static class IHeaderDictionaryExtensions
{
    public static bool TryGetInitialSession(this IHeaderDictionary source, out string? sessionId)
    {
        if (source.TryGetValue(SessionHeaders.InitializeSession, out var value))
        {
            sessionId = value.First();
            return true;
        }

        sessionId = null;
        return false;
    }

    public static bool TryGetPreviousSession(this IHeaderDictionary source, out string? sessionId)
    {
        if (source.TryGetValue(SessionHeaders.FromPreviousSession, out var value))
        {
            sessionId = value.First();
            return true;
        }

        sessionId = null;
        return false;
    }

    public static bool TryGetSession(this IHeaderDictionary source, out string? sessionId)
    {
        if (source.TryGetValue(SessionHeaders.Session, out var value))
        {
            sessionId = value.First();
            return true;
        }

        sessionId = null;
        return false;
    }
}