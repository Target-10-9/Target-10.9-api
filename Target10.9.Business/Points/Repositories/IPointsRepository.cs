using Target10._9.Business.Points.Entities;

namespace Target10._9.Business.Points.Repositories;

public interface IPointsRepository
{
    #region Get
    Task<List<Point>> GetPointsAsync(Guid userId, CancellationToken cancellationToken);
    #endregion
    
    #region Post
    Task<Point> AddPointAsync(
        float X_Coordinate,
        float Y_Coordinate,
        DateTime dateTimePoint,
        Guid sessionId,
        CancellationToken cancellationToken
    );
    #endregion
}