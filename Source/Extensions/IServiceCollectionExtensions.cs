using Source.Session;

namespace Source.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddSessionManager(this IServiceCollection services)
    {
        services.AddSingleton<ISessionLocker, SessionLocker>();
        services.AddScoped(typeof(ISessionManager<>), typeof(SessionManager<>));
        services.AddScoped(typeof(ISessionService<>), typeof(SessionService<>));
        services.AddScoped(typeof(ISessionValidator<>), typeof(SessionValidator<>));
    }
}