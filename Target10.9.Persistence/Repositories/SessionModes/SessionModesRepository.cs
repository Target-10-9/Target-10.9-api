using Microsoft.EntityFrameworkCore;
using Target10._9.Business.SessionModes.Entities;
using Target10._9.Business.SessionModes.Repositories;

namespace Target10._9.Persistence.Repositories.SessionModes;

public class SessionModesRepository(ApplicationDbContext dbContext) : ISessionModesRepository
{
    #region Get
    
    public Task<List<SessionMode>> GetSessionModesAsync(CancellationToken cancellationToken)
    {
        return dbContext.SessionModes.ToListAsync(cancellationToken);
    }
    
    public Task<SessionMode> GetSessionModeByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return dbContext.SessionModes.FirstOrDefaultAsync(sm => sm.Id == id, cancellationToken);
    }
    
    #endregion
    
    #region Post
    
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