using Microsoft.EntityFrameworkCore;
using Target10._9.Business.Points.Entities;
using Target10._9.Business.Points.Repositories;

namespace Target10._9.Persistence.Repositories.Points;

public class PointsRepository(ApplicationDbContext dbContext) : IPointsRepository
{
    #region GET
    
    public Task<List<Point>> GetPointsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return dbContext.Points
            .Include(p => p.Session)
            .Where(p => p.Session.UserId == userId)
            .ToListAsync(cancellationToken);
    }
    
    public Task<List<Point>> GetPointsBySessionIdAsync(Guid sessionId, Guid userId, CancellationToken cancellationToken)
    {
        return dbContext.Points
            .Include(p => p.Session)
            .Where(p => p.SessionId == sessionId && p.Session.UserId == userId)
            .ToListAsync(cancellationToken);
    }
    
    #endregion
    
    #region POST
    
    public async Task<Point> AddPointAsync(
        float X_Coordinate,
        float Y_Coordinate,
        Guid sessionId,
        CancellationToken cancellationToken
    )
    {
        var point = new Point
        {
            Id = Guid.NewGuid(),
            X_Coordinate = X_Coordinate,
            Y_Coordinate = Y_Coordinate,
            DateTimePoint = DateTime.UtcNow,
            SessionId = sessionId
        };
        
        dbContext.Points.Add(point);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return point;
    }
    
    #endregion
    
    #region DELETE
    
    public async Task DeletePointByIdAsync(Guid pointId, Guid userId, CancellationToken cancellationToken)
    {
        var point = await dbContext.Points
            .Include(p => p.Session)
            .FirstOrDefaultAsync(p => p.Id == pointId && p.Session.UserId == userId, cancellationToken);
        
        if (point == null)
            throw new KeyNotFoundException("Point not found.");

        dbContext.Points.Remove(point);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    
    #endregion
}