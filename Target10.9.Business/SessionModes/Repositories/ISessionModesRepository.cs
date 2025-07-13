using Target10._9.Business.SessionModes.Entities;

namespace Target10._9.Business.SessionModes.Repositories;

public interface ISessionModesRepository
{
    #region Get
    Task<List<SessionMode>> GetSessionModesAsync(Guid userId, CancellationToken cancellationToken);
    #endregion
}