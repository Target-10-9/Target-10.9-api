using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target10._9.Business.SessionModes;
using Target10._9.Business.SessionModes.Commands;
using Target10._9.Business.SessionModes.Queries;
using Target10._9.Business.SessionModeWeaponDetails.Commands;
using Target10._9.Business.SessionModeWeaponDetails.Queries;

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
        var sessionMode = await sessionModesService.GetSessionModeByIdAsync(query, User, cancellationToken);

        return Ok(sessionMode);
    }
    
    [HttpGet("{sessionModeId:guid}/weapons")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuthorizedWeapons(Guid sessionModeId, CancellationToken cancellationToken)
    {
        var query = new GetAuthorizedWeaponsQuery { Id = sessionModeId };
        var result = await sessionModesService.GetAuthorizedWeaponsAsync(query, cancellationToken);
        return Ok(result);
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
    
    [HttpPost("{sessionModeId:guid}/weapons")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAuthorizedWeapon(Guid sessionModeId, [FromBody] AddAuthorizedWeaponCommand command, CancellationToken cancellationToken)
    {
        await sessionModesService.AddAuthorizedWeaponAsync(sessionModeId, command, cancellationToken);
        return NoContent();
    }

    
    #endregion
    
    #region Put
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSessionMode(Guid id, [FromBody] UpdateSessionModeByIdCommand command, CancellationToken cancellationToken)
    {
        var result = await sessionModesService.UpdateSessionModeByIdAsync(id, command, User, cancellationToken);
        return Ok(result);
    }
    
    #endregion
    
    #region Delete
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSessionMode(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteSessionModeByIdCommand { Id = id };
        await sessionModesService.DeleteSessionModeByIdAsync(command, User, cancellationToken);
        return NoContent();
    }
    
    #endregion
}