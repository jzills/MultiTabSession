using Microsoft.AspNetCore.SignalR;
using Source.Session;

namespace Source.Hubs;

public class SessionHub : Hub<ISessionHub>
{
    private static readonly Dictionary<string, string> _sessionTabConnections = new Dictionary<string, string>();
    private readonly ISessionManager<SessionTab> _sessionManager;

    public SessionHub(ISessionManager<SessionTab> sessionManager) =>
        _sessionManager = sessionManager;

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        if (httpContext == null)
        {
            throw new Exception("No HttpContext configured.");
        }

        if (httpContext.Request.Query
            .TryGetValue(SessionHeader.Session, out var sessionValue))
        {
            var sessionId = sessionValue.First();
            _sessionTabConnections[Context.ConnectionId] = sessionId;

            var callerSessionTabNotifications = _sessionTabConnections
                .Where(item => item.Key != Context.ConnectionId)
                .Select(item => item.Value);

            await Clients.Caller.Created(callerSessionTabNotifications);
            await Clients.Others.Created(new[] { sessionId });
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (_sessionTabConnections
                .Remove(Context.ConnectionId, out var clientSessionId))
        {
            await Clients.Others.Removed(clientSessionId);
        }

        await base.OnDisconnectedAsync(exception);
    }
}