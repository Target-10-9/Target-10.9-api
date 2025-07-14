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

    #endregion
}