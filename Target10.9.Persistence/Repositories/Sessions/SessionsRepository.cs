using Microsoft.EntityFrameworkCore;
using Target10._9.Business.Sessions.Entities;
using Target10._9.Business.Sessions.Repositories;

namespace Target10._9.Persistence.Repositories.Sessions;

public class SessionsRepository(ApplicationDbContext dbContext) : ISessionsRepository
{
    #region Get

    public Task<List<Session>> GetSessionsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return dbContext.Sessions
            .Where(s => s.UserId == userId)
            .ToListAsync(cancellationToken);
    }
    
    public Task<Session?> GetSessionByIdAsync(Guid sessionId, Guid userId, CancellationToken cancellationToken)
    {
        return dbContext.Sessions
            .FirstOrDefaultAsync(s => s.Id == sessionId && s.UserId == userId, cancellationToken);
    }

    #endregion
    
    #region Post
    
    public async Task<Session> AddSessionAsync(
        Guid userId,
        string name,
        DateTime dateStart,
        DateTime dateEnd,
        Guid sessionModeId,
        CancellationToken cancellationToken
    )
    {
        var session = new Session
        {
            Id = Guid.NewGuid(),
            Name = name,
            DateStart = dateStart,
            DateEnd = dateEnd,
            UserId = userId,
            SessionModeId = sessionModeId
        };
        
        dbContext.Sessions.Add(session);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return session;
    }
    
    #endregion
    
    #region Put
    
    public async Task<Session?> UpdateSessionByIdAsync(
        Guid sessionId,
        Guid userId,
        string name,
        DateTime dateStart,
        DateTime dateEnd,
        Guid sessionModeId,
        CancellationToken cancellationToken
    )
    {
        var session = await dbContext.Sessions
            .FirstOrDefaultAsync(s => s.Id == sessionId && s.UserId == userId, cancellationToken);
        
        if (session == null)
            return null;

        session.Name = name;
        session.DateStart = dateStart;
        session.DateEnd = dateEnd;
        session.SessionModeId = sessionModeId;

        dbContext.Sessions.Update(session);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return session;
    }
    
    #endregion
}