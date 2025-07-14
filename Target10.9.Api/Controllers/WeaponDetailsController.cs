using Microsoft.AspNetCore.Mvc;
using Target10._9.Business.WeaponDetails;
using Target10._9.Business.WeaponDetails.Commands;

namespace Target10._9_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeaponDetailsController(
    IWeaponDetailsService weaponDetailsService
    ) : ControllerBase
{
    #region Get
    
    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWeaponDetails(CancellationToken cancellationToken)
    {
        var weaponDetails = await weaponDetailsService.GetWeaponDetails(User, cancellationToken);
        return Ok(weaponDetails);
    }
    
    #endregion
    
    #region POST
    
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddWeaponDetails([FromBody] WeaponDetailCommand command, CancellationToken cancellationToken)
    {
        var result = await weaponDetailsService.AddWeaponDetails(command, User, cancellationToken);
        return Ok(result);
    }
    
    #endregion
}