using Target10._9.Business.WeaponDetails.Entities;

namespace Target10._9.Business.WeaponDetails.Repositories;

public interface IWeaponDetailsRepository
{
    #region Get
    
    Task<List<WeaponDetail>> GetWeaponDetailsAsync(Guid userId, CancellationToken cancellationToken);
    
    #endregion
}