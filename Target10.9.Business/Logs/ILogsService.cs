using System.Security.Claims;
using AutoMapper;
using Target10._9.Business.Logs.Repositories;
using Target10._9.Business.Logs.Responses;

namespace Target10._9.Business.Logs
{
    public interface ILogsService
    {
        #region Get
        Task<List<GetLogsResponse>> GetLogsAsync(ClaimsPrincipal user, CancellationToken cancellationToken);
        #endregion
    }

    public class LogsService(
        ILogsRepository logsRepository,
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
        
        #endregion
    }

    // --------- LOG ----------
    // internal static partial class AccountServiceLoggerExtension
    // {
    //     private const int EventIdOffset = 1000;
    //
    //     [LoggerMessage(
    //         EventId = EventIdOffset + 0,
    //         Level = LogLevel.Information,
    //         Message = "Connexion de l'utilisateur {user}.")]
    //     public static partial void UserLogin(this ILogger logger, string user);
    // }
}
