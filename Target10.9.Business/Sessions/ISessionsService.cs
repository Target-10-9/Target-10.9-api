using System.Security.Claims;
using AutoMapper;
using Target10._9.Business.Sessions.Repositories;
using Target10._9.Business.Sessions.Responses;

namespace Target10._9.Business.Sessions
{
    public interface ISessionsService
    {
        #region Get
        Task<List<GetSessionsResponse>> GetSessions(ClaimsPrincipal user, CancellationToken cancellationToken);
        #endregion
    }

    public class SessionsService(
        ISessionsRepository sessionsRepository,
        IMapper mapper
        ) : ISessionsService
    {
        #region Get

        public async Task<List<GetSessionsResponse>> GetSessions(ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var weapons = await sessionsRepository.GetSessionsAsync(userIdGuid, cancellationToken);

            return mapper.Map<List<GetSessionsResponse>>(weapons);
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
