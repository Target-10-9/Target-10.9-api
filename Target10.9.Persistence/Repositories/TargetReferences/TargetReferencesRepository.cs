using Microsoft.EntityFrameworkCore;
using Target10._9.Business.TargetReferences.Repositories;

namespace Target10._9.Persistence.Repositories.TargetReferences;

public class TargetReferencesRepository (ApplicationDbContext dbContext) : ITargetReferencesRepository
{
    #region CHECK
    
    public async Task<bool> CheckIfTargetReferenceIdExistAsync(Guid targetReferenceId, CancellationToken cancellationToken)
    {
        return await dbContext.TargetReferences
            .AnyAsync(t => t.TargetReferenceId == targetReferenceId, cancellationToken);
    }
    
    #endregion
}