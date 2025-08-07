using System.Security.Claims;
using AutoMapper;
using Target10._9.Business.Points.Repositories;
using Target10._9.Business.Points.Responses;

namespace Target10._9.Business.Points
{
    public interface IPointsService
    {
        #region Get
        Task<List<GetPointsResponse>> GetPointsAsync(ClaimsPrincipal user, CancellationToken cancellationToken);
        #endregion
    }

    public class PointsService(
        IPointsRepository pointsRepository,
        IMapper mapper
        ) : IPointsService
    {
        #region Get
        
        public async Task<List<GetPointsResponse>> GetPointsAsync(ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var points = await pointsRepository.GetPointsAsync(userIdGuid, cancellationToken);

            return mapper.Map<List<GetPointsResponse>>(points);
        }
        
        #endregion
    }
}
