using Microsoft.AspNetCore.Mvc;
using MultiTabSession.Session;

namespace MultiTabSession.Controllers;

[Route("task")]
public class TaskController : Controller
{
    private static object _locker = new object();
    private readonly ISessionLocker _sessionLocker;
    private readonly ISessionManager<SessionTab> _sessionManager;

    public TaskController(
        ISessionManager<SessionTab> sessionManager,
        ISessionLocker sessionLocker) => (_sessionManager, _sessionLocker
    ) = (sessionManager, sessionLocker);

    [HttpGet]
    [Route("infinite")]
    public IActionResult Infinite()
    {
        lock (_locker)
        {
            while (true)
            {
                var current = _sessionManager.Current;
            }
        }
    }

    [HttpGet]
    [Route("short")]
    public IActionResult Short()
    {
        lock (_locker)
        {
            Thread.Sleep(5000);
            var current = _sessionManager.Current;
            return Ok(current);
        }
    }

    [HttpGet]
    [Route("long")]
    public IActionResult Long(string key)
    {
        lock (_sessionLocker.Get(key)!)
        {
            Thread.Sleep(30000);
            var current = _sessionManager.Current;
            return Ok(current);
        }
    }

    [HttpGet]
    [Route("lock")]
    public IActionResult Lock(string key)
    {
        _sessionLocker.Add(key);
        return Ok();
    }
}