using System.Security.Claims;
using AutoMapper;
using Target10._9.Business.Services;
using Target10._9.Business.WeaponDetails.Commands;
using Target10._9.Business.WeaponDetails.Repositories;
using Target10._9.Business.WeaponDetails.Responses;

namespace Target10._9.Business.WeaponDetails
{
    public interface IWeaponDetailsService
    {
        #region Get
        Task<List<GetWeaponDetailsResponse>> GetWeaponDetails(ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        Task<GetWeaponDetailResponse> GetWeaponDetailsById(Guid id, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
        
        #region POST
        Task<AddWeaponDetailResponse> AddWeaponDetail(AddWeaponDetailCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
        
        #region PUT
        Task<UpdateWeaponDetailResponse> UpdateWeaponDetail(Guid id, UpdateWeaponDetailCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
        
        #region Delete
        Task DeleteWeaponDetail(DeleteWeaponCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
    }

    public class WeaponDetailsService(
        IWeaponDetailsRepository weaponDetailsRepository,
        IValidationService validationService,
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
        
        public async Task<GetWeaponDetailResponse> GetWeaponDetailsById(Guid id, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");

            var weapon = await weaponDetailsRepository.GetWeaponDetailByIdAsync(id, userIdGuid, cancellationToken);
            
            if (weapon == null)
                return null;

            return mapper.Map<GetWeaponDetailResponse>(weapon);
        }
        
        #endregion
        
        #region POST
        
        public async Task<AddWeaponDetailResponse> AddWeaponDetail(AddWeaponDetailCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(command, cancellationToken);
            
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
            
            return mapper.Map<AddWeaponDetailResponse>(response);
        }
        
        #endregion
        
        #region PUT
        
        public async Task<UpdateWeaponDetailResponse> UpdateWeaponDetail(Guid id, UpdateWeaponDetailCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(command, cancellationToken);
            
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
            
            return mapper.Map<UpdateWeaponDetailResponse>(response);
        }
        
        #endregion
        
        #region Delete
        
        public async Task DeleteWeaponDetail(DeleteWeaponCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(command, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");

            await weaponDetailsRepository.DeleteWeaponDetailAsync(command.Id, userIdGuid, cancellationToken);
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
