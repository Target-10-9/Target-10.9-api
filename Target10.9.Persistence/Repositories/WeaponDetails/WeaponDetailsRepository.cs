using Microsoft.EntityFrameworkCore;
using Target10._9.Business.WeaponDetails.Entities;
using Target10._9.Business.WeaponDetails.Repositories;

namespace Target10._9.Persistence.Repositories.WeaponDetails;

public class WeaponDetailsRepository(ApplicationDbContext dbContext) : IWeaponDetailsRepository
{
    #region Get

    public Task<List<WeaponDetail>> GetWeaponDetailsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return dbContext.WeaponDetails
            .Where(w => w.UserId == userId)
            .ToListAsync(cancellationToken);
    }
    
    public Task<WeaponDetail?> GetWeaponDetailByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        return dbContext.WeaponDetails
            .FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId, cancellationToken);
    }

    #endregion
    
    #region Post
    
    public async Task<WeaponDetail> AddWeaponDetailAsync(
        Guid userId,
        string name,
        string brand,
        string description,
        string serialNumber,
        CancellationToken cancellationToken
    )
    {
        var weaponDetail = new WeaponDetail
        {
            Id = Guid.NewGuid(),
            Name = name,
            Brand = brand,
            Description = description,
            SerialNumber = serialNumber,
            UserId = userId
        };

        dbContext.WeaponDetails.Add(weaponDetail);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return weaponDetail;
    }
    
    #endregion
    
    #region Put
    
    public async Task<WeaponDetail> UpdateWeaponDetailByIdAsync(
        Guid id,
        Guid userId,
        string name,
        string brand,
        string description,
        string serialNumber,
        CancellationToken cancellationToken
    )
    {
        var weaponDetail = await dbContext.WeaponDetails
            .FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId, cancellationToken);

        if (weaponDetail == null)
            throw new KeyNotFoundException("Weapon detail not found.");

        weaponDetail.Name = name;
        weaponDetail.Brand = brand;
        weaponDetail.Description = description;
        weaponDetail.SerialNumber = serialNumber;

        dbContext.WeaponDetails.Update(weaponDetail);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return weaponDetail;
    }
    
    #endregion
    
    #region Delete
    
    public async Task DeleteWeaponDetailAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        var weaponDetail = await dbContext.WeaponDetails
            .FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId, cancellationToken);

        if (weaponDetail == null)
            throw new KeyNotFoundException("Weapon detail not found.");

        dbContext.WeaponDetails.Remove(weaponDetail);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    
    #endregion
}