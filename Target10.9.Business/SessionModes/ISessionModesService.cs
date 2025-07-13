using System.Security.Claims;
using AutoMapper;
using Target10._9.Business.Services;
using Target10._9.Business.SessionModes.Entities;
using Target10._9.Business.SessionModes.Repositories;
using Target10._9.Business.SessionModes.Responses;

namespace Target10._9.Business.SessionModes
{
    public interface ISessionModesService
    {
        #region Get
        Task<List<GetSessionModesResponse>> GetSessionModesAsync(ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
    }

    public class SessionModesService(
        ISessionModesRepository sessionModesRepository,
        IMapper mapper
        ) : ISessionModesService
    {
        #region Get
        
        public async Task<List<GetSessionModesResponse>> GetSessionModesAsync(ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var sessionModes = await sessionModesRepository.GetSessionModesAsync(userIdGuid, cancellationToken);

            return mapper.Map<List<GetSessionModesResponse>>(sessionModes);
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
