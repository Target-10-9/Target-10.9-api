using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target10._9.Business.Points;
using Target10._9.Business.Points.Commands;
using Target10._9.Business.Points.Queries;

namespace Target10._9_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PointController(
    IPointsService pointsService
    ) : ControllerBase
{
    #region Get
    
    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSessions(CancellationToken cancellationToken)
    {
        var sessions = await pointsService.GetPointsAsync(User, cancellationToken);
        return Ok(sessions);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSessionbyId(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetPointByIdQuery { Id = id };
        var sessions = await pointsService.GetPointByIdAsync(query, User, cancellationToken);
        return Ok(sessions);
    }
    
    #endregion
    
    #region Post
    
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddPoint([FromBody] AddPointCommand command, CancellationToken cancellationToken)
    {
        var result = await pointsService.AddPointAsync(command, User, cancellationToken);
        return Ok(result);
    }
    
    #endregion
}