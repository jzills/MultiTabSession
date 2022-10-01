using Microsoft.AspNetCore.SignalR;
using Source.Extensions;

namespace Source.Hubs;

public class SessionHub : Hub<ISessionHub>
{
    private readonly IConnectionManager _connectionManager;

    public SessionHub(IConnectionManager connectionManager) =>
        _connectionManager = connectionManager;

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        if (httpContext == null)
        {
            throw new Exception("No HttpContext configured.");
        }

        if (httpContext.Request.Query.TryGetSessionValue(out var sessionId))
        {
            _connectionManager.Add(Context.ConnectionId, sessionId);

            await Clients.Caller.OnCreatedAsync(
                _connectionManager.AllValuesExcept(Context.ConnectionId)
            );

            await Clients.Others.OnCreatedAsync(
                _connectionManager.ValuesFor(Context.ConnectionId)
            );
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (_connectionManager.Remove(Context.ConnectionId, out var clientSessionId))
        {
            if (!string.IsNullOrEmpty(clientSessionId))
            {
                await Clients.Others.OnRemovedAsync(clientSessionId);
            }
        }

        await base.OnDisconnectedAsync(exception);
    }
}