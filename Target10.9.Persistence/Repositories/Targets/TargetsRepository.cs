using Microsoft.EntityFrameworkCore;
using Target10._9.Business.Targets.Entities;
using Target10._9.Business.Targets.Repositories;

namespace Target10._9.Persistence.Repositories.Targets;

public class TargetsRepository (ApplicationDbContext dbContext) : ITargetsRepository
{
    #region CHECK
    
    public async Task<bool> CheckIfTargetUserExistAsync(Guid targetId, Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.Targets
            .AnyAsync(t => t.TargetId == targetId && t.UserId == userId, cancellationToken);
    }
    
    #endregion
    
    #region POST
    
    public async Task<Target> AddTargetUserAsync(Guid targetId, Guid userId, CancellationToken cancellationToken)
    {
        var target = new Target
        {
            Id = Guid.NewGuid(),
            TargetId = targetId,
            UserId = userId,
        };
        
        dbContext.Targets.Add(target);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return target;
    }
    
    #endregion
    
    #region DELETE
    
    public async Task DeleteTargetUserAsync(Guid targetId, Guid userId, CancellationToken cancellationToken)
    {
        var target = await dbContext.Targets
            .FirstOrDefaultAsync(t => t.TargetId == targetId && t.UserId == userId, cancellationToken);
        
        if (target != null)
        {
            dbContext.Targets.Remove(target);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
    
    #endregion
}