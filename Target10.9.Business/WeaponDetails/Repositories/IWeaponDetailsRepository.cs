using Target10._9.Business.WeaponDetails.Entities;

namespace Target10._9.Business.WeaponDetails.Repositories;

public interface IWeaponDetailsRepository
{
    #region Get
    Task<List<WeaponDetail>> GetWeaponDetailsAsync(Guid userId, CancellationToken cancellationToken);
    #endregion

    #region POST
    Task<WeaponDetail> AddWeaponDetailAsync(
        Guid userId,
        string name,
        string brand,
        string description,
        string serialNumber,
        CancellationToken cancellationToken
    );
    #endregion
    
    #region PUT
    Task<WeaponDetail> UpdateWeaponDetailAsync(
        Guid id,
        Guid userId,
        string name,
        string brand,
        string description,
        string serialNumber,
        CancellationToken cancellationToken
    );
    #endregion
}