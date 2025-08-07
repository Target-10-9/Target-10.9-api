using Target10._9.Business.Points.Entities;

namespace Target10._9.Business.Points.Repositories;

public interface IPointsRepository
{
    #region Get
    Task<List<Point>> GetPointsAsync(Guid userId, CancellationToken cancellationToken);
    #endregion
}