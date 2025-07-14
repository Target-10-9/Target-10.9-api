using Target10._9.Business.ModeDetails.Entities;

namespace Target10._9.Business.ModeDetails.Repositories;

public interface IModeDetailsRepository
{
    #region Get
    Task<List<ModeDetail>> GetModeDetailsAsync(CancellationToken cancellationToken);
    Task<ModeDetail> GetModeDetailByIdAsync(Guid id, CancellationToken cancellationToken);
    #endregion
    
    #region Post
    Task<ModeDetail> AddModeDetailAsync(
        int shootLimit,
        TimeOnly shootingTime,
        TimeOnly restTime,
        CancellationToken cancellationToken
    );
    #endregion
    
    #region Put
    Task<ModeDetail> UpdateModeDetailByIdAsync(
        Guid id, 
        int ShootLimit,
        TimeOnly ShootingTime,
        TimeOnly RestTime,
        CancellationToken cancellationToken
    );
    #endregion
}