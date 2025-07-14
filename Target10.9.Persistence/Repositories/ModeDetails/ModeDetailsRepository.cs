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
}