using MultiTabSession;
using MultiTabSession.Hubs;
using MultiTabSession.Session;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromHours(1));
builder.Services.AddScoped(typeof(ISessionManager<>), typeof(SessionManager<>));
builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder
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
app.UseMiddleware<RequestSessionMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}"
);

app.MapHub<SessionHub>("/hubs");

app.MapFallbackToFile("index.html");

app.Run();
