using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target10._9.Business.Points;
using Target10._9.Business.Points.Commands;

namespace Target10._9_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PointController(
    IPointsService pointsService
    ) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSessions(CancellationToken cancellationToken)
    {
        var sessions = await pointsService.GetPointsAsync(User, cancellationToken);
        return Ok(sessions);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddPoint([FromBody] AddPointCommand command, CancellationToken cancellationToken)
    {
        var result = await pointsService.AddPointAsync(command, User, cancellationToken);
        return Ok(result);
    }
}