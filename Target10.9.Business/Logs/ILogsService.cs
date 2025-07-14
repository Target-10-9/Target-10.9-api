using System.Security.Claims;
using AutoMapper;
using Target10._9.Business.Logs.Queries;
using Target10._9.Business.Logs.Repositories;
using Target10._9.Business.Logs.Responses;
using Target10._9.Business.Services;

namespace Target10._9.Business.Logs
{
    public interface ILogsService
    {
        #region Get
        Task<List<GetLogsResponse>> GetLogsAsync(ClaimsPrincipal user, CancellationToken cancellationToken);
        Task<GetLogByIdResponse> GetLogByIdAsync(GetLogByIdQuery query, ClaimsPrincipal user, CancellationToken cancellationToken);
        #endregion
    }

    public class LogsService(
        ILogsRepository logsRepository,
        IValidationService validationService,
        IMapper mapper
        ) : ILogsService
    {
        #region Get
        
        public async Task<List<GetLogsResponse>> GetLogsAsync(ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var logs = await logsRepository.GetLogsAsync(userIdGuid, cancellationToken);

            return mapper.Map<List<GetLogsResponse>>(logs);
        }
        
        public async Task<GetLogByIdResponse> GetLogByIdAsync(GetLogByIdQuery query, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(query, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var log = await logsRepository.GetLogByIdAsync(query.Id, userIdGuid, cancellationToken);
            
            return mapper.Map<GetLogByIdResponse>(log);
        }
        
        #endregion
    }
}
