using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target10._9.Business.Users;
using Target10._9.Business.Users.Commands;
using Target10._9.Business.Users.Responses;

namespace Target10._9_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(
    IUsersService usersService
    ) : ControllerBase
{
    #region Get

    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser(Guid id, CancellationToken cancellationToken)
    {
        var user = await usersService.GetUserByIdAsync(id, cancellationToken);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    #endregion

    #region Update

    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(UpdateUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await usersService.UpdateUserAsync(id, request, User, cancellationToken);
            return Ok(response);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }

    #endregion
}
