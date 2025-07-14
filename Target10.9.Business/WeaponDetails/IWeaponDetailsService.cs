using System.Security.Claims;
using AutoMapper;
using Target10._9.Business.Services;
using Target10._9.Business.Users;
using Target10._9.Business.WeaponDetails.Repositories;
using Target10._9.Business.WeaponDetails.Responses;

namespace Target10._9.Business.WeaponDetails
{
    public interface IWeaponDetailsService
    {
        Task<List<GetWeaponDetailsResponse>> GetWeaponDetails(ClaimsPrincipal currentUser, CancellationToken cancellationToken);
    }

    public class WeaponDetailsService(
        IWeaponDetailsRepository weaponDetailsRepository,
        IMapper mapper
        ) : IWeaponDetailsService
    {
        #region Get
        
        public async Task<List<GetWeaponDetailsResponse>> GetWeaponDetails(ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var weapons = await weaponDetailsRepository.GetWeaponDetailsAsync(userIdGuid, cancellationToken);

            return mapper.Map<List<GetWeaponDetailsResponse>>(weapons);
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
