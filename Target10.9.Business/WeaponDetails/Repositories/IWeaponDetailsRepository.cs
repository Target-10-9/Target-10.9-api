using Target10._9.Business.WeaponDetails.Entities;

namespace Target10._9.Business.WeaponDetails.Repositories;

public interface IWeaponDetailsRepository
{
    #region Get
    Task<List<WeaponDetail>> GetWeaponDetailsAsync(Guid userId, CancellationToken cancellationToken);
    Task<WeaponDetail> GetWeaponDetailByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    #endregion

    #region Post
    Task<WeaponDetail> AddWeaponDetailAsync(
        Guid userId,
        string name,
        string brand,
        string description,
        string serialNumber,
        CancellationToken cancellationToken
    );
    #endregion
    
    #region Put
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
    
    #region Delete
    Task DeleteWeaponDetailAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    #endregion
}