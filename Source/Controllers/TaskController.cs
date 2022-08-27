using Microsoft.AspNetCore.Mvc;
using MultiTabSession.Session;

namespace MultiTabSession.Controllers;

public class TaskController : Controller
{
    private readonly ISessionManager<SessionTab> _sessionManager;

    public TaskController(ISessionManager<SessionTab> sessionManager) => _sessionManager = sessionManager;

    [Route("infinite")]
    public IActionResult Infinite()
    {
        while (true)
        {
            var current = _sessionManager.Current;
        }
    }
}