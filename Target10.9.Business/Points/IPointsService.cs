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
        #region GET
        Task<List<GetPointsResponse>> GetPointsAsync(ClaimsPrincipal user, CancellationToken cancellationToken);

        Task<List<GetPointsBySessionIdResponse>> GetPointsBySessionIdAsync(GetPointsBySessionIdQuery query, ClaimsPrincipal currentUser,
            CancellationToken cancellationToken);
        #endregion
        
        #region POST
        Task<AddPointResponse> AddPointAsync(AddPointCommand command, ClaimsPrincipal currentUser,
            CancellationToken cancellationToken);
        #endregion
        
        #region DELETE

        Task DeletePointByIdAsync(DeletePointByIdCommand command, ClaimsPrincipal currentUser,
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
        #region GET
        
        public async Task<List<GetPointsResponse>> GetPointsAsync(ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var points = await pointsRepository.GetPointsAsync(userIdGuid, cancellationToken);

            return mapper.Map<List<GetPointsResponse>>(points);
        }
        
        public async Task<List<GetPointsBySessionIdResponse>> GetPointsBySessionIdAsync(GetPointsBySessionIdQuery query, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(query, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var points = await pointsRepository.GetPointsBySessionIdAsync(query.Id, userIdGuid, cancellationToken);
            
            if (points == null)
                throw new KeyNotFoundException("Point not found.");

            return mapper.Map<List<GetPointsBySessionIdResponse>>(points);
        }
        
        #endregion
        
        #region POST
        
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
                "Point Created",
                $"Point created by user {userIdGuid}.",
                userIdGuid,
                cancellationToken
            );

            return mapper.Map<AddPointResponse>(pointResponse);
        }
        
        #endregion
        
        #region DELETE
        
        public async Task DeletePointByIdAsync(DeletePointByIdCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(command, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");

            await pointsRepository.DeletePointByIdAsync(command.Id, userIdGuid, cancellationToken);
            
            await logsRepository.AddLogAsync(
                "Point Deleted",
                $"Point with ID {command.Id} deleted by user {userIdGuid}.",
                userIdGuid,
                cancellationToken
            );
        }
        
        #endregion
    }
}
