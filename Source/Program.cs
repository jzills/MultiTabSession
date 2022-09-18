using Source.Extensions;
using Source.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddMemoryCache(options => options.ExpirationScanFrequency = TimeSpan.FromSeconds(10));
builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromHours(1));
builder.Services.AddSessionManager();

builder.Services.AddSignalR();
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
