using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Target10._9.Business.WeaponDetails;
using Target10._9.Business.WeaponDetails.Commands;
using Target10._9.Business.WeaponDetails.Queries;

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
        var weaponDetails = await weaponDetailsService.GetWeaponDetailsAsync(User, cancellationToken);
        return Ok(weaponDetails);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWeaponDetailsById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetWeaponDetailByIdQuery { Id = id };
        var weaponDetail = await weaponDetailsService.GetWeaponDetailsByIdAsync(query, User, cancellationToken);
        if (weaponDetail == null)
        {
            return NotFound();
        }
        return Ok(weaponDetail);
    }
    
    #endregion
    
    #region POST
    
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddWeaponDetails([FromBody] AddWeaponDetailCommand command, CancellationToken cancellationToken)
    {
        var result = await weaponDetailsService.AddWeaponDetailAsync(command, User, cancellationToken);
        return Ok(result);
    }
    
    #endregion
    
    #region PUT
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateWeaponDetails(Guid id, [FromBody] UpdateWeaponDetailByIdCommand command, CancellationToken cancellationToken)
    {
        var result = await weaponDetailsService.UpdateWeaponDetailByIdAsync(id, command, User, cancellationToken);
        return Ok(result);
    }
    
    #endregion
    
    #region Delete
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteWeaponDetails(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteWeaponDetailByIdCommand { Id = id };
        await weaponDetailsService.DeleteWeaponDetailByIdAsync(command, User, cancellationToken);
        return NoContent();
    }
    
    #endregion
}