using Source.Session;

namespace Source.Hubs;

public interface ISessionHub
{
    Task Notify(IEnumerable<SessionTab>? sessionTabs);
}