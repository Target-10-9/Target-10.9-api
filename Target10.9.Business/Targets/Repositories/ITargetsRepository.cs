using Target10._9.Business.Targets.Entities;

namespace Target10._9.Business.Targets.Repositories;

public interface ITargetsRepository
{
    #region CHECK
    Task<bool> CheckIfTargetUserExistAsync(Guid targetId, Guid userId, CancellationToken cancellationToken);
    #endregion
    #region POST
    Task<Target> AddTargetUserAsync(Guid targetId, Guid userId, CancellationToken cancellationToken);
    #endregion
    
    #region DELETE
    Task DeleteTargetUserAsync(Guid targetId, Guid userId, CancellationToken cancellationToken);
    #endregion
}