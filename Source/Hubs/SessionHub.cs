using Microsoft.AspNetCore.SignalR;
using MultiTabSession.Session;

namespace MultiTabSession.Hubs;

public class SessionHub : Hub<ISessionHub>
{
    private readonly ISessionManager<SessionTab> _sessionManager;

    public SessionHub(ISessionManager<SessionTab> sessionManager) =>
        _sessionManager = sessionManager;

    public async Task Notify() => await Clients.All.Notify(_sessionManager.Get());
}