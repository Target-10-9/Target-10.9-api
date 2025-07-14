using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target10._9.Business.Users;
using Target10._9.Business.Users.Commands;
using Target10._9.Business.Users.Queries;
using Target10._9.Business.Users.Responses;

namespace Target10._9_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController(
    IUsersService usersService
    ) : ControllerBase
{
    #region Get

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetUserByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery { Id = id };
        
        var user = await usersService.GetUserByIdAsync(query, User, cancellationToken);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    #endregion

    #region Update

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UpdateUserByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserByIdCommand byIdCommand, CancellationToken cancellationToken)
    {
        try
        {
            var response = await usersService.UpdateUserAsync(id, byIdCommand, User, cancellationToken);
            return Ok(response);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }

    #endregion

    #region Delete

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteUserByIdCommand { Id = id };
            await usersService.DeleteUserAsync(command, User, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    #endregion
}
