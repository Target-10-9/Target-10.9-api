using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Target10._9.Business.Accounts;
using Target10._9.Business.Accounts.Commands;

namespace Target10._9_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(
        IAccountsService accountService
    ) : ControllerBase
    {
        /// <summary>
        /// Inscription d'un nouvel utilisateur.
        /// </summary>
        /// <param name="request">Les informations d'inscription.</param>
        /// <returns>Confirmation de l'inscription.</returns>
        [HttpPost("register")]
        [SwaggerResponse(200, "Utilisateur inscrit avec succès")]
        [SwaggerResponse(400, "Requête invalide")]
        [SwaggerResponse(409, "Email déjà utilisé")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await accountService.RegisterAsync(request, cancellationToken);
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
    }
}
