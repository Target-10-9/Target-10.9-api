using Microsoft.EntityFrameworkCore;
using Target10._9.Business.ModeDetails.Entities;
using Target10._9.Business.ModeDetails.Repositories;

namespace Target10._9.Persistence.Repositories.ModeDetails;

public class ModeDetailsRepository(ApplicationDbContext dbContext) : IModeDetailsRepository
{
    #region Get
    
    public Task<List<ModeDetail>> GetModeDetailsAsync(CancellationToken cancellationToken)
    {
        return dbContext.ModeDetails.ToListAsync(cancellationToken);
    }
    
    public Task<ModeDetail?> GetModeDetailByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return dbContext.ModeDetails.FirstOrDefaultAsync(md => md.Id == id, cancellationToken);
    }
    
    #endregion
    
    #region Post
    
    public async Task<ModeDetail> AddModeDetailAsync(
        int shootLimit,
        TimeOnly shootingTime,
        TimeOnly restTime,
        CancellationToken cancellationToken
    )
    {
        var modeDetail = new ModeDetail
        {
            ShootLimit = shootLimit,
            ShootingTime = shootingTime,
            RestTime = restTime
        };

        await dbContext.ModeDetails.AddAsync(modeDetail, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return modeDetail;
    }
    
    #endregion
    
    #region Put
    
    public async Task<ModeDetail> UpdateModeDetailByIdAsync(
        Guid id, 
        int shootLimit,
        TimeOnly shootingTime,
        TimeOnly restTime,
        CancellationToken cancellationToken
    )
    {
        var modeDetail = await dbContext.ModeDetails.FindAsync(new object[] { id }, cancellationToken);
        
        if (modeDetail == null)
            throw new KeyNotFoundException($"ModeDetail with ID {id} not found.");
        
        modeDetail.ShootLimit = shootLimit;
        modeDetail.ShootingTime = shootingTime;
        modeDetail.RestTime = restTime;

        dbContext.ModeDetails.Update(modeDetail);
        await dbContext.SaveChangesAsync(cancellationToken);

        return modeDetail;
    }
    
    #endregion
    
    #region Delete
    
    public async Task DeleteModeDetailByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var modeDetail = await dbContext.ModeDetails.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        
        if (modeDetail == null)
            throw new KeyNotFoundException($"ModeDetail with ID {id} not found.");
        
        dbContext.ModeDetails.Remove(modeDetail);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    
    #endregion
}