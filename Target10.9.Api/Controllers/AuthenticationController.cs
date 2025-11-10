using Microsoft.AspNetCore.Mvc;
using Target10._9.Business.Authentications;
using Target10._9.Business.Authentications.Commands;
using Target10._9.Business.Authentications.Responses;

namespace Target10._9_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(
        IAuthenticationsService authenticationsService
    ) : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var result = await authenticationsService.RegisterAsync(command, cancellationToken);
                return Ok(result);
            }
           //  catch (InvalidOperationException ex)
           //  {
           //      return Conflict(new { message = "Erreur typé confli : " + ex.Message });
           //  }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "Erreur typé Mauvaise requete : " + ex.Message });
            }
        }
        
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
        { 
            var response = await authenticationsService.LoginAsync(command, cancellationToken);
            return Ok(response);
        }
    }
}
