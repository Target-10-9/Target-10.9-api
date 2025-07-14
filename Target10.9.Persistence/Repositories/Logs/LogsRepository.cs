using Microsoft.EntityFrameworkCore;
using Target10._9.Business.Logs.Entities;
using Target10._9.Business.Logs.Repositories;

namespace Target10._9.Persistence.Repositories.Logs;

public class LogsRepository(ApplicationDbContext dbContext) : ILogsRepository
{
    #region Get
    
    public Task<List<Log>> GetLogsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return dbContext.Logs
            .Where(log => log.UserId == userId)
            .ToListAsync(cancellationToken);
    }
    
    public Task<Log> GetLogByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        return dbContext.Logs.FirstOrDefaultAsync(log => log.Id == id && log.UserId == userId, cancellationToken);
    }
    
    #endregion
}