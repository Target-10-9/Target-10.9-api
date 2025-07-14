using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target10._9.Business.Sessions;
using Target10._9.Business.Sessions.Commands;
using Target10._9.Business.Sessions.Queries;

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
        var sessions = await sessionsService.GetSessionsAsync(User, cancellationToken);
        return Ok(sessions);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSessionbyId(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetSessionByIdQuery { Id = id };
        var sessions = await sessionsService.GetSessionByIdAsync(query, User, cancellationToken);
        return Ok(sessions);
    }

    #endregion
    
    #region Post
    
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddSession([FromBody] AddSessionCommand command, CancellationToken cancellationToken)
    {
        var result = await sessionsService.AddSessionAsync(command, User, cancellationToken);
        return Ok(result);
    }
    
    #endregion
    
    #region Put
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSessionById(Guid id, [FromBody] UpdateSessionByIdCommand command, CancellationToken cancellationToken)
    {
        var result = await sessionsService.UpateSessionByIdAsync(id, command, User, cancellationToken);
        return Ok(result);
    }
    
    #endregion
}