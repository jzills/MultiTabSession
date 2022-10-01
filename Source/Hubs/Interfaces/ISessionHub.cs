using Microsoft.AspNetCore.SignalR;

namespace Source.Hubs;

public interface ISessionHub
{
    [HubMethodName("created")]
    Task OnCreatedAsync(IEnumerable<string> clientSessionIds);
    
    [HubMethodName("removed")]
    Task OnRemovedAsync(string clientSessionId);
}