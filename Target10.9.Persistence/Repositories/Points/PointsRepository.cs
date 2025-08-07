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
    
    public Task<Point?> GetPointByIdAsync(Guid pointId, Guid userId, CancellationToken cancellationToken)
    {
        return dbContext.Points
            .Include(p => p.Session)
            .FirstOrDefaultAsync(p => p.Id == pointId && p.Session.UserId == userId, cancellationToken);
    }
    
    #endregion
    
    #region Post
    
    public async Task<Point> AddPointAsync(
        float X_Coordinate,
        float Y_Coordinate,
        DateTime dateTimePoint,
        Guid sessionId,
        CancellationToken cancellationToken
    )
    {
        var point = new Point
        {
            Id = Guid.NewGuid(),
            X_Coordinate = X_Coordinate,
            Y_Coordinate = Y_Coordinate,
            DateTimePoint = dateTimePoint,
            SessionId = sessionId
        };
        
        dbContext.Points.Add(point);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return point;
    }
    
    #endregion
}