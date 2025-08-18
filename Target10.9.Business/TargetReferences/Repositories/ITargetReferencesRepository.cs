namespace Target10._9.Business.TargetReferences.Repositories;

public interface ITargetReferencesRepository
{
    #region CHECK
    Task<bool> CheckIfTargetReferenceIdExistAsync(Guid targetReferenceId, CancellationToken cancellationToken);
    #endregion
}