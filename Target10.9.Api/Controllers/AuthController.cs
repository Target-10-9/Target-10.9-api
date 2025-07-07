using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Target10._9_api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Inscription d'un nouvel utilisateur.
        /// </summary>
        /// <param name="request">Les informations d'inscription.</param>
        /// <returns>Confirmation de l'inscription.</returns>
        [HttpPost("register")]
        [SwaggerResponse(200, "Utilisateur inscrit avec succès")]
        [SwaggerResponse(400, "Requête invalide")]
        public IActionResult Register()
        {
            return Ok("Utilisateur inscrit avec succès.");
        }
    }
}
