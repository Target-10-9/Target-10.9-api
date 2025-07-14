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
        /// <summary>
        /// Inscription d'un nouvel utilisateur.
        /// </summary>
        /// <param name="request">Les informations d'inscription.</param>
        /// <returns>Confirmation de l'inscription.</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await authenticationsService.RegisterAsync(request, cancellationToken);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = "Cet email est déjà utilisé" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await authenticationsService.LoginAsync(request, cancellationToken);
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Email ou mot de passe incorrect" });
            }
        }
    }
}
