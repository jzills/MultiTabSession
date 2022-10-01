using Source.Session;

namespace Source.Hubs;

public interface ISessionHub
{
    Task Created(IEnumerable<string> clientSessionIds);
    Task Removed(string clientSessionId);
    Task Expiration(DateTime expiresIn);
}