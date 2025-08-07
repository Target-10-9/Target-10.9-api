using System.Security.Claims;
using AutoMapper;
using Target10._9.Business.Logs.Repositories;
using Target10._9.Business.Points.Commands;
using Target10._9.Business.Points.Queries;
using Target10._9.Business.Points.Repositories;
using Target10._9.Business.Points.Responses;
using Target10._9.Business.Services;

namespace Target10._9.Business.Points
{
    public interface IPointsService
    {
        #region Get
        Task<List<GetPointsResponse>> GetPointsAsync(ClaimsPrincipal user, CancellationToken cancellationToken);

        Task<GetPointByIdResponse> GetPointByIdAsync(GetPointByIdQuery query, ClaimsPrincipal currentUser,
            CancellationToken cancellationToken);
        #endregion
        
        #region Post

        Task<AddPointResponse> AddPointAsync(AddPointCommand command, ClaimsPrincipal currentUser,
            CancellationToken cancellationToken);

        #endregion
    }

    public class PointsService(
        IPointsRepository pointsRepository,
        IValidationService validationService,
        ILogsRepository logsRepository,
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
        
        public async Task<GetPointByIdResponse> GetPointByIdAsync(GetPointByIdQuery query, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(query, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var point = await pointsRepository.GetPointByIdAsync(query.Id, userIdGuid, cancellationToken);
            
            if (point == null)
                throw new KeyNotFoundException("Session not found.");

            return mapper.Map<GetPointByIdResponse>(point);
        }
        
        #endregion
        
        #region Post
        
        public async Task<AddPointResponse> AddPointAsync(AddPointCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(command, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");

            var pointResponse = await pointsRepository.AddPointAsync(
                command.X_Coordinate,
                command.Y_Coordinate,
                command.DateTimePoint,
                command.SessionId,
                cancellationToken
            );
            
            await logsRepository.AddLogAsync(
                "Session Created",
                $"Point created by user {userIdGuid}.",
                userIdGuid,
                cancellationToken
            );

            return mapper.Map<AddPointResponse>(pointResponse);
        }
        
        #endregion
    }
}
