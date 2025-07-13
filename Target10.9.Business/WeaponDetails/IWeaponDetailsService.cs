using System.Security.Claims;
using AutoMapper;
using Target10._9.Business.WeaponDetails.Commands;
using Target10._9.Business.WeaponDetails.Repositories;
using Target10._9.Business.WeaponDetails.Responses;

namespace Target10._9.Business.WeaponDetails
{
    public interface IWeaponDetailsService
    {
        #region Get
        Task<List<GetWeaponDetailsResponse>> GetWeaponDetails(ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
        
        #region POST
        Task<WeaponDetailsResponse> AddWeaponDetails(WeaponDetailCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
        
        #region PUT
        Task<WeaponDetailsResponse> UpdateWeaponDetails(Guid id, UpdateWeaponDetailCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
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
        
        #region POST
        
        public async Task<WeaponDetailsResponse> AddWeaponDetails(WeaponDetailCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");

            var response = await weaponDetailsRepository.AddWeaponDetailAsync(
                userIdGuid,
                command.Name,
                command.Brand,
                command.Description,
                command.SerialNumber,
                cancellationToken
            );
            
            return mapper.Map<WeaponDetailsResponse>(response);
        }
        
        #endregion
        
        #region PUT
        
        public async Task<WeaponDetailsResponse> UpdateWeaponDetails(Guid id, UpdateWeaponDetailCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");

            var response = await weaponDetailsRepository.UpdateWeaponDetailAsync(
                id,
                userIdGuid,
                command.Name,
                command.Brand,
                command.Description,
                command.SerialNumber,
                cancellationToken
            );
            
            return mapper.Map<WeaponDetailsResponse>(response);
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
