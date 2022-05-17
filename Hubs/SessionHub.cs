using Microsoft.AspNetCore.SignalR;
using MultiTabSession.Session;

namespace MultiTabSession.Hubs;

public interface ISessionHub
{
    Task Notify(IEnumerable<SessionTab>? sessionTabs);
}

public class SessionHub : Hub<ISessionHub>
{
    private readonly ISessionManager<SessionTab> _sessionManager;

    public SessionHub(ISessionManager<SessionTab> sessionManager) =>
        _sessionManager = sessionManager;

    public async Task Notify() => await Clients.All.Notify(_sessionManager.GetSessions());
}