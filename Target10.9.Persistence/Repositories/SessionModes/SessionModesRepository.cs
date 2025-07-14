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
    
    public Task<SessionMode?> GetSessionModeByIdAsync(Guid id, CancellationToken cancellationToken)
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
    
    #region Put
    
    public async Task<SessionMode> UpdateSessionModeAsync(
        Guid id,
        string name,
        TimeOnly timeLimits,
        TimeOnly warmUp,
        string discipline,
        Guid modeDetailId,
        CancellationToken cancellationToken
    )
    {
        var sessionMode = await GetSessionModeByIdAsync(id, cancellationToken);
        
        if (sessionMode == null)
            throw new KeyNotFoundException("Session mode not found.");
        
        sessionMode.Name = name;
        sessionMode.TimeLimits = timeLimits;
        sessionMode.WarmUp = warmUp;
        sessionMode.Discipline = discipline;
        sessionMode.ModeDetailId = modeDetailId;
        
        dbContext.SessionModes.Update(sessionMode);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return sessionMode;
    }
    
    #endregion
    
    #region Delete
    
    public async Task DeleteSessionModeByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var sessionMode = await dbContext.SessionModes.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        
        if (sessionMode == null)
            throw new KeyNotFoundException("Session mode not found.");
        
        dbContext.SessionModes.Remove(sessionMode);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    
    #endregion
}