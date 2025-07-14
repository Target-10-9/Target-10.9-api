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
    
    public Task<ModeDetail> GetModeDetailByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return dbContext.ModeDetails.FirstOrDefaultAsync(md => md.Id == id, cancellationToken);
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
}