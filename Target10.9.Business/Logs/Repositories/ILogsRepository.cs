using Target10._9.Business.Logs.Entities;

namespace Target10._9.Business.Logs.Repositories;

public interface ILogsRepository
{
    #region Get
    Task<List<Log>> GetLogsAsync(Guid userId, CancellationToken cancellationToken);
    Task<Log> GetLogByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    #endregion
}