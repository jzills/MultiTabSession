using Microsoft.AspNetCore.Mvc;
using Source.Extensions;
using Source.Session;

namespace Source.Controllers;

[Route("applicationState")]
public class ApplicationStateController : Controller
{
    private readonly ISessionManager<SessionTab> _sessionManager;

    public ApplicationStateController(ISessionManager<SessionTab> sessionManager) =>
        _sessionManager = sessionManager;

    [HttpPost]
    public IActionResult BatchUpdate([FromBody]Dictionary<string, string> applicationState)
    {
        if (Request.Headers.TryGetSessionValue(out var sessionId))
        {
            var session = _sessionManager.Get(sessionId);
            if (session != null)
            {
                session.ApplicationState = applicationState;
                _sessionManager.Update(sessionId, session);
            }

            return Ok();
        }

        return BadRequest();
    }
}