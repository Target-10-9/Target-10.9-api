using System.Security.Claims;
using AutoMapper;
using Target10._9.Business.ModeDetails.Repositories;
using Target10._9.Business.ModeDetails.Responses;

namespace Target10._9.Business.ModeDetails
{
    public interface IModeDetailsService
    {
        #region Get
        Task<List<GetModeDetailsResponse>> GetModeDetailsAsync(ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
    }

    public class ModeDetailsService(
        IModeDetailsRepository modeDetailsRepository,
        IMapper mapper
        ) : IModeDetailsService
    {
        #region Get
        
        public async Task<List<GetModeDetailsResponse>> GetModeDetailsAsync(ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var sessionModes = await modeDetailsRepository.GetModeDetailsAsync(cancellationToken);

            return mapper.Map<List<GetModeDetailsResponse>>(sessionModes);
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
