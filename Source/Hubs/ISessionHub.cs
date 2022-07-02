using MultiTabSession.Session;

namespace MultiTabSession.Hubs;

public interface ISessionHub
{
    Task Notify(IEnumerable<SessionTab>? sessionTabs);
}