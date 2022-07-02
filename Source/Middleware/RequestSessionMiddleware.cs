using MultiTabSession.Extensions;
using MultiTabSession.Session;

namespace MultiTabSession.Middleware;

public class RequestSessionMiddleware
{
    private readonly RequestDelegate _next;

    public RequestSessionMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context, ISessionManager<SessionTab> sessionManager)
    {
        if (context.Request.Headers.TryGetSessionHeader(SessionHeader.Session, out var sessionId))
#pragma warning disable CS8604
            sessionManager.SetCurrent(sessionId);
#pragma warning restore CS8604
            
        await _next(context);
    }
}