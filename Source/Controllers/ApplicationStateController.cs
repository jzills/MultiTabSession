using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MultiTabSession.Extensions;
using MultiTabSession.Session;

namespace MultiTabSession.Controllers;

[Route("applicationState")]
public class ApplicationStateController : Controller
{
    private readonly ISessionManager<SessionTab> _sessionManager;

    public ApplicationStateController(ISessionManager<SessionTab> sessionManager) =>
        _sessionManager = sessionManager;

    [HttpPost]
    public IActionResult BatchUpdate([FromBody]Dictionary<string, string> applicationState)
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
}