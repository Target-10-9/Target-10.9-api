using Target10._9.Business.Sessions.Entities;

namespace Target10._9.Business.Sessions.Repositories;

public interface ISessionsRepository
{
    #region Get
    Task<List<Session>> GetSessionsAsync(Guid userId, CancellationToken cancellationToken);
    #endregion
}