using Microsoft.EntityFrameworkCore;
using Target10._9.Business.Points.Entities;
using Target10._9.Business.Points.Repositories;

namespace Target10._9.Persistence.Repositories.Points;

public class PointsRepository(ApplicationDbContext dbContext) : IPointsRepository
{
    #region Get
    
    public Task<List<Point>> GetPointsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return dbContext.Points
            .Include(p => p.Session)
            .Where(p => p.Session.UserId == userId)
            .ToListAsync(cancellationToken);
    }
    
    #endregion
}