using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target10._9.Business.WeaponDetails;
using Target10._9.Business.WeaponDetails.Commands;

namespace Target10._9_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WeaponDetailController(
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
    
    #region PUT
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateWeaponDetails(Guid id, [FromBody] UpdateWeaponDetailCommand command, CancellationToken cancellationToken)
    {
        var result = await weaponDetailsService.UpdateWeaponDetails(id, command, User, cancellationToken);
        return Ok(result);
    }
    
    #endregion
}