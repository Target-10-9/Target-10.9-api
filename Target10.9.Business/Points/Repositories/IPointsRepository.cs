using Target10._9.Business.Points.Entities;
using Target10._9.Business.Sessions.Entities;

namespace Target10._9.Business.Points.Repositories;

public interface IPointsRepository
{
    #region GET
    Task<List<Point>> GetPointsAsync(Guid userId, CancellationToken cancellationToken);
    Task<List<Point>> GetPointsBySessionIdAsync(Guid sessionId, Guid userId, CancellationToken cancellationToken);
    #endregion
    
    #region POST
    Task<Point> AddPointAsync(
        float X_Coordinate,
        float Y_Coordinate,
        Guid sessionId,
        CancellationToken cancellationToken
    );
    #endregion
    
    #region DELETE
    Task DeletePointByIdAsync(Guid pointId, Guid userId, CancellationToken cancellationToken);
    #endregion
}