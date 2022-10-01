using Source.Extensions;
using Source.Hubs;
using Source.Session;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddMemoryCache(options => options.ExpirationScanFrequency = TimeSpan.FromMinutes(10));
builder.Services.AddSession(options => 
{
    options.Cookie.Name = "SessionManager.Client";
    options.IdleTimeout = TimeSpan.FromMinutes(SessionConfiguration.SlidingExpirationInMinutes);
});
builder.Services.AddSessionManager();

builder.Services.AddSignalR();
builder.Services.AddSingleton<IConnectionManager, ConnectionManager>();
builder.Services.AddControllersWithViews();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(options =>
    {
        options
            .WithOrigins("https://localhost:44413/")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors();
app.UseRouting();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}"
);

app.MapHub<SessionHub>("/hubs");

app.MapFallbackToFile("index.html");

app.Run();
