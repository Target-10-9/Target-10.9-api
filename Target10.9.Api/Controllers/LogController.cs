using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target10._9.Business.Logs;
using Target10._9.Business.Logs.Queries;

namespace Target10._9_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LogController(
    ILogsService logsService
    ) : ControllerBase
{
    #region Get
    
    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLogs(CancellationToken cancellationToken)
    {
        var logs = await logsService.GetLogsAsync(User, cancellationToken);
        return Ok(logs);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLogById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetLogByIdQuery { Id = id };
        var log = await logsService.GetLogByIdAsync(query, User, cancellationToken);
        return Ok(log);
    }
    
    #endregion
}