using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target10._9.Business.Sessions;
using Target10._9.Business.Sessions.Commands;

namespace Target10._9_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SessionController(
    ISessionsService sessionsService
    ) : ControllerBase
{
    #region Get

    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSessions(CancellationToken cancellationToken)
    {
        var sessions = await sessionsService.GetSessions(User, cancellationToken);
        return Ok(sessions);
    }

    #endregion
    
    #region Post
    
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PostSession([FromBody] AddSessionCommand command, CancellationToken cancellationToken)
    {
        var result = await sessionsService.AddSession(command, User, cancellationToken);
        return Ok(result);
    }
    
    #endregion
}