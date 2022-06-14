using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MultiTabSession.Extensions;
using MultiTabSession.Hubs;
using MultiTabSession.Session;

namespace MultiTabSession.Controllers;

[Route("session")]
public class SessionController : Controller
{
    private readonly ISessionManager<SessionTab> _sessionManager;
    private readonly IHubContext<SessionHub, ISessionHub> _sessionHub;

    public SessionController(
        ISessionManager<SessionTab> sessionManager,
        IHubContext<SessionHub, ISessionHub> sessionHub
    ) => (_sessionManager, _sessionHub) = (sessionManager, sessionHub);

    [HttpPost]
    public IActionResult AddSession()
    {
        if (Request.Headers.TryGetInitialSession(out var sessionId))
        {
            if (Request.Headers.TryGetPreviousSession(out var previousSessionId))
            {
            #pragma warning disable CS8604

                var previousSession = _sessionManager.GetSession(previousSessionId);
                if (previousSession != null)
                    _sessionManager.AddSession(sessionId, previousSession);

            #pragma warning restore CS8604

                else _sessionManager.RemoveSession(previousSessionId);
            } 
            else
            {
            #pragma warning disable CS8604

                _sessionManager.AddSession(sessionId, new SessionTab
                {
                    ApplicationState = new Dictionary<string, string>
                    {
                        // Current application state values
                        ["UserId"] = DateTime.Now.Millisecond.ToString(),
                        ["AccountId"] = DateTime.Now.Millisecond.ToString()
                    }
                });
                
            #pragma warning restore CS8604
            }
 
            _sessionHub.Clients.All.Notify(_sessionManager.GetSessions());

            return RedirectToAction("Index", "Home");
        }

        return BadRequest("Missing required headers.");
    }

    [HttpGet]
    public IActionResult GetSession() => Ok(_sessionManager.Current);

    [HttpGet]
    [Route("all")]
    public IActionResult GetSessions() => Ok(_sessionManager.GetSessions());

    [HttpGet]
    [Route("window")]
    public IActionResult GetWindowName() => Ok(new { WindowName = Guid.NewGuid() });
}
