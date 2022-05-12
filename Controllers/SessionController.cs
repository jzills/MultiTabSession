using Microsoft.AspNetCore.Mvc;
using MultiTabSession.Extensions;
using MultiTabSession.Session;

namespace MultiTabSession.Controllers;

public class SessionController : Controller
{
    private readonly ISessionManager<SessionTab> _sessionManager;

    public SessionController(ISessionManager<SessionTab> sessionManager) =>
        _sessionManager = sessionManager;

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
                {
                    previousSession.Id = DateTime.Now.Millisecond;
                    previousSession.WindowName = Guid.Parse(sessionId);
                    _sessionManager.AddSession(sessionId, previousSession);
                    #pragma warning restore CS8604
                } 
                else
                {
                    _sessionManager.RemoveSession(previousSessionId);
                }
            } 
            else
            {
                #pragma warning disable CS8604
                _sessionManager.AddSession(sessionId, new SessionTab
                {
                    Id = DateTime.Now.Millisecond,          
                    WindowName = Guid.Parse(sessionId),
                    ApplicationState = new Dictionary<string, string>
                    {
                        // Current application state values
                        ["UserId"] = DateTime.Now.Millisecond.ToString(),
                        ["AccountId"] = DateTime.Now.Millisecond.ToString()
                    }
                });
                #pragma warning restore CS8604
            }

            return RedirectToAction("Index", "Home");
        }

        return BadRequest("Missing required headers.");
    }

    [HttpPost]
    public IActionResult BatchUpdateApplicationState([FromBody]Dictionary<string, string> applicationState)
    {
        if (Request.Headers.TryGetSession(out var sessionId))
        {
            #pragma warning disable CS8604
            var session = _sessionManager.GetSession(sessionId);
            #pragma warning restore CS8604

            if (session != null)
            {
                session.ApplicationState = applicationState;
                _sessionManager.UpdateSession(sessionId, session);
            }

            return Ok();
        }

        return BadRequest();
    }

    public IActionResult GetSession()
    {
        if (Request.Headers.TryGetSession(out var sessionId))
        #pragma warning disable CS8604
            return Ok(_sessionManager.GetSession(sessionId));
        #pragma warning restore CS8604

        return BadRequest();
    }

    public IActionResult GetWindowName() => Ok(new { WindowName = Guid.NewGuid() });

    public IActionResult VerifySession(string sessionId) => Ok(_sessionManager.GetSession(sessionId));
}
