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
        if (Request.Headers.TryGetSessionHeader(SessionHeader.InitializeSession, out var sessionId))
        {
            if (Request.Headers.TryGetSessionHeader(SessionHeader.FromPreviousSession, out var previousSessionId))
            {
#pragma warning disable CS8604
                var previousSession = _sessionManager.Get(previousSessionId);
                if (previousSession != null)
                    _sessionManager.Add(sessionId, previousSession);
#pragma warning restore CS8604

                else _sessionManager.Remove(previousSessionId);
            } 
            else
            {
#pragma warning disable CS8604
                _sessionManager.Add(sessionId, new SessionTab
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
 
            _sessionHub.Clients.All.Notify(_sessionManager.Get());

            return RedirectToAction("Index", "Home");
        }

        return BadRequest("Missing required headers.");
    }

    [HttpGet]
    public IActionResult GetSession() => Ok(_sessionManager.Current);

    [HttpGet]
    [Route("all")]
    public IActionResult GetSessions() => Ok(_sessionManager.Get());

    [HttpGet]
    [Route("window")]
    public IActionResult GetWindowName() => Ok(new { WindowName = Guid.NewGuid() });
}