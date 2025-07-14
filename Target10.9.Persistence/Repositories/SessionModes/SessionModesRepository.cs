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
    
    #region POST
    
    public async Task<SessionMode> AddSessionModeAsync(
        string name,
        TimeOnly timeLimits,
        TimeOnly warmUp,
        string discipline,
        Guid modeDetailId,
        CancellationToken cancellationToken
    )
    {
        var sessionMode = new SessionMode
        {
            Name = name,
            TimeLimits = timeLimits,
            WarmUp = warmUp,
            Discipline = discipline,
            ModeDetailId = modeDetailId
        };
        
        dbContext.SessionModes.Add(sessionMode);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return sessionMode;
    }
    
    #endregion
}