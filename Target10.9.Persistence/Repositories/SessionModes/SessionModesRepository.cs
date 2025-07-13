using Microsoft.EntityFrameworkCore;
using Target10._9.Business.SessionModes.Entities;
using Target10._9.Business.SessionModes.Repositories;

namespace Target10._9.Persistence.Repositories.SessionModes;

public class SessionModesRepository(ApplicationDbContext dbContext) : ISessionModesRepository
{
    #region Get
    
    public Task<List<SessionMode>> GetSessionModesAsync(Guid userId, CancellationToken cancellationToken)
    {
        return dbContext.SessionModes
            .Include(sm => sm.Sessions)
            .Where(sm => sm.Sessions.Any(s => s.UserId == userId))
            .ToListAsync(cancellationToken);
    }
    
    #endregion
}