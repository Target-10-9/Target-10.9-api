using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target10._9.Business.SessionModes;
using Target10._9.Business.SessionModes.Commands;
using Target10._9.Business.SessionModes.Queries;

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
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSessionModeById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetSessionModeByIdQuery { Id = id };
        var weaponDetail = await sessionModesService.GetSessionModeByIdAsync(query, User, cancellationToken);
        if (weaponDetail == null)
        {
            return NotFound();
        }
        return Ok(weaponDetail);
    }
    
    
    #endregion
    
    #region Post
    
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddSessionMode([FromBody] AddSessionModeCommand command, CancellationToken cancellationToken)
    {
        var result = await sessionModesService.AddSessionModeAsync(command, User, cancellationToken);
        return Ok(result);
    }
    
    #endregion
}