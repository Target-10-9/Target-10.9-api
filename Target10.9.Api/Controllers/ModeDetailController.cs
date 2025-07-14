using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target10._9.Business.ModeDetails;

namespace Target10._9_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ModeDetailController(
    IModeDetailsService modeDetailsService
    ) : ControllerBase
{
    #region Get
    
    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetModeDetails(CancellationToken cancellationToken)
    {
        var sessionModes = await modeDetailsService.GetModeDetailsAsync(User, cancellationToken);
        return Ok(sessionModes);
    }
    
    #endregion
}