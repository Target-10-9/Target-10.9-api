using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target10._9.Business.SessionModes;

namespace Target10._9_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SessionModeController(
    ISessionModesService sessionModesService
    ) : ControllerBase
{
    #region Get
    
    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSessionModes(CancellationToken cancellationToken)
    {
        var sessionModes = await sessionModesService.GetSessionModesAsync(User, cancellationToken);
        return Ok(sessionModes);
    }
    
    #endregion
}