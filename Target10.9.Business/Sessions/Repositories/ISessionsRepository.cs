using Target10._9.Business.Sessions.Entities;

namespace Target10._9.Business.Sessions.Repositories;

public interface ISessionsRepository
{
    #region GET
    Task<List<Session>> GetSessionsAsync(Guid userId, CancellationToken cancellationToken);
    Task<Session?> GetSessionByIdAsync(Guid sessionId, Guid userId, CancellationToken cancellationToken);
    Task<bool> CheckIfSessionEtatInProgressExistAsync(Guid userId, CancellationToken cancellationToken);
    Task<Session?> GetSessionIdByEtatInProgressAsync(Guid userId, CancellationToken cancellationToken);
    #endregion
    
    #region POST
    Task<Session> AddSessionAsync(
        Guid userId,
        string name,
        DateTime dateStart,
        DateTime dateEnd,
        Guid sessionModeId,
        CancellationToken cancellationToken
    );
    #endregion
    
    #region PUT
    Task<Session?> UpdateSessionByIdAsync(
        Guid sessionId,
        Guid userId,
        string name,
        DateTime dateStart,
        DateTime dateEnd,
        Guid sessionModeId,
        SessionEtat etat,
        CancellationToken cancellationToken
    );
    #endregion
    
    #region DELETE
    Task DeleteSessionByIdAsync(Guid sessionId, Guid userId, CancellationToken cancellationToken);
    #endregion
}