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
    #region GET
    
    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPoints(CancellationToken cancellationToken)
    {
        var points = await pointsService.GetPointsAsync(User, cancellationToken);
        return Ok(points);
    }
    
    [HttpGet("{sessionId}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPointsBySessionId(Guid sessionId, CancellationToken cancellationToken)
    {
        var query = new GetPointsBySessionIdQuery { Id = sessionId };
        var points = await pointsService.GetPointsBySessionIdAsync(query, User, cancellationToken);
        return Ok(points);
    }
    
    #endregion
    
    #region POST
    
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddPoint([FromBody] AddPointCommand command, CancellationToken cancellationToken)
    {
        var result = await pointsService.AddPointAsync(command, User, cancellationToken);
        return Ok(result);
    }
    
    #endregion
    
    #region DELETE
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePointById(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeletePointByIdCommand { Id = id };
        await pointsService.DeletePointByIdAsync(command, User, cancellationToken);
        return NoContent();
    }
    
    #endregion
}