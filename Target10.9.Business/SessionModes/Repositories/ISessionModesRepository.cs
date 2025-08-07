using Target10._9.Business.SessionModes.Entities;
using Target10._9.Business.WeaponDetails.Entities;

namespace Target10._9.Business.SessionModes.Repositories;

public interface ISessionModesRepository
{
    #region Get
    Task<List<SessionMode>> GetSessionModesAsync(CancellationToken cancellationToken);
    Task<SessionMode?> GetSessionModeByIdAsync(Guid id, CancellationToken cancellationToken);

    public Task<List<WeaponDetail>> GetAuthorizedWeaponsAsync(Guid sessionModeId,
        CancellationToken cancellationToken);
    #endregion
    
    #region Post
    Task<SessionMode> AddSessionModeAsync(
        string name,
        TimeOnly timeLimits,
        TimeOnly warmUp,
        string discipline,
        Guid modeDetailId,
        CancellationToken cancellationToken
    );

    Task AddAuthorizedWeaponAsync(
        Guid sessionModeId, 
        Guid weaponDetailId,
        CancellationToken cancellationToken
    );
    #endregion
    
    #region Put
    Task<SessionMode> UpdateSessionModeAsync(
        Guid id,
        string name,
        TimeOnly timeLimits,
        TimeOnly warmUp,
        string discipline,
        Guid modeDetailId,
        CancellationToken cancellationToken
    );
    #endregion
    
    #region Delete
    Task DeleteSessionModeByIdAsync(Guid id, CancellationToken cancellationToken);
    #endregion
}