using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Source.Extensions;
using Source.Hubs;
using Source.Session;

namespace Source.Controllers;

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
        if (Request.Headers.TryGetInitialSessionValue(out var sessionId))
        {
            if (Request.Headers.TryGetPreviousSessionValue(out var previousSessionId))
            {
                _sessionManager.CopyFrom(sessionId, previousSessionId);
            } 
            else
            {
                _sessionManager.Add(sessionId, new SessionTab
                {
                    ApplicationState = new Dictionary<string, string>
                    {
                        // Current application state values
                        ["UserId"] = DateTime.Now.Millisecond.ToString(),
                        ["AccountId"] = DateTime.Now.Millisecond.ToString()
                    }
                });     
            }

            return RedirectToAction("Index", "Home");
        }

        return BadRequest("Missing required headers.");
    }

    [HttpDelete]
    public IActionResult RemoveSession()
    {
        if (Request.Headers.TryGetSessionValue(out var sessionId))
        {
            _sessionManager.Remove(sessionId);
            return Ok("Session removed successfully.");
        }

        return BadRequest("No session found to remove.");
    }

    [HttpGet]
    public IActionResult GetSession() => Ok(_sessionManager.Current);

    [HttpGet]
    [Route("all")]
    public IActionResult GetSessions() => Ok(_sessionManager.GetAll());

    [HttpGet]
    [Route("window")]
    public IActionResult GetClientSessionId() => Ok(new { ClientSessionId = Guid.NewGuid() });
}