using Microsoft.AspNetCore.Mvc;
using MultiTabSession.Session;

namespace MultiTabSession.Controllers;

[Route("locks")]
public class SessionLockerController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISessionLocker _sessionLocker;

    public SessionLockerController(
        IHttpContextAccessor httpContextAccessor,
        ISessionLocker sessionLocker
    ) => (_httpContextAccessor, _sessionLocker) = (httpContextAccessor, sessionLocker);

    [HttpGet]
    [Route("add")]
    public IActionResult Add()
    {
        var sessionId = _httpContextAccessor.HttpContext!.Session.Id;
        _sessionLocker.Add(sessionId);
        return Ok(sessionId);
    }

    [HttpGet]
    public IActionResult Get() => Ok(_sessionLocker.GetKeys());
}