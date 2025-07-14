using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target10._9.Business.ModeDetails;
using Target10._9.Business.ModeDetails.Commands;
using Target10._9.Business.ModeDetails.Queries;

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
        var modeDetails = await modeDetailsService.GetModeDetailsAsync(User, cancellationToken);
        return Ok(modeDetails);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetModeDetailById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetModeDetailByIdQuery { Id = id };
        var modeDetail = await modeDetailsService.GetModeDetailByIdAsync(query, User, cancellationToken);

        return Ok(modeDetail);
    }
    
    #endregion
    
    #region Post
    
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddModeDetailMode([FromBody] AddModeDetailCommand command, CancellationToken cancellationToken)
    {
        var result = await modeDetailsService.AddModeDetailAsync(command, User, cancellationToken);
        return Ok(result);
    }
    
    #endregion
    
    
    #region Put
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateModeDetail(Guid id, [FromBody] UpdateModeDetailByIdCommand command, CancellationToken cancellationToken)
    {
        var result = await modeDetailsService.UpdateModeDetailByIdAsync(id, command, User, cancellationToken);

        return Ok(result);
    }
    
    #endregion
}