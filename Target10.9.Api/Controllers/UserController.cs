using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target10._9.Business.Users;
using Target10._9.Business.Users.Responses;

namespace Target10._9_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(
    IUsersService usersService
    ) : ControllerBase
{
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await usersService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }
}
