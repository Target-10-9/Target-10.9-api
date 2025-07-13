using Target10._9.Business.Sessions.Entities;

namespace Target10._9.Business.Sessions.Repositories;

public interface ISessionsRepository
{
    #region Get
    Task<List<Session>> GetSessionsAsync(Guid userId, CancellationToken cancellationToken);
    Task<Session?> GetSessionByIdAsync(Guid sessionId, Guid userId, CancellationToken cancellationToken);
    #endregion
    
    #region Post
    Task<Session> AddSessionAsync(
        Guid userId,
        string name,
        DateTime dateStart,
        DateTime dateEnd,
        Guid sessionModeId,
        CancellationToken cancellationToken
    );
    #endregion
}