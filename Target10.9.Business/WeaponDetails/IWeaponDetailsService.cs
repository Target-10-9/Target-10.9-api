using System.Security.Claims;
using AutoMapper;
using Target10._9.Business.Services;
using Target10._9.Business.WeaponDetails.Commands;
using Target10._9.Business.WeaponDetails.Queries;
using Target10._9.Business.WeaponDetails.Repositories;
using Target10._9.Business.WeaponDetails.Responses;

namespace Target10._9.Business.WeaponDetails
{
    public interface IWeaponDetailsService
    {
        #region Get
        Task<List<GetWeaponDetailsResponse>> GetWeaponDetailsAsync(ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        Task<GetWeaponDetailByIdResponse> GetWeaponDetailsByIdAsync(GetWeaponDetailByIdQuery query, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
        
        #region POST
        Task<AddWeaponDetailResponse> AddWeaponDetailAsync(AddWeaponDetailCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
        
        #region PUT
        Task<UpdateWeaponDetailByIdResponse> UpdateWeaponDetailByIdAsync(Guid id, UpdateWeaponDetailByIdCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
        
        #region Delete
        Task DeleteWeaponDetailByIdAsync(DeleteWeaponDetailByIdCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
    }

    public class WeaponDetailsService(
        IWeaponDetailsRepository weaponDetailsRepository,
        IValidationService validationService,
        IMapper mapper
        ) : IWeaponDetailsService
    {
        #region Get
        
        public async Task<List<GetWeaponDetailsResponse>> GetWeaponDetailsAsync(ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var weapons = await weaponDetailsRepository.GetWeaponDetailsAsync(userIdGuid, cancellationToken);

            return mapper.Map<List<GetWeaponDetailsResponse>>(weapons);
        }
        
        public async Task<GetWeaponDetailByIdResponse> GetWeaponDetailsByIdAsync(GetWeaponDetailByIdQuery query,  ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(query, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");

            var weapon = await weaponDetailsRepository.GetWeaponDetailByIdAsync(query.Id, userIdGuid, cancellationToken);
            
            if (weapon == null)
                return null;

            return mapper.Map<GetWeaponDetailByIdResponse>(weapon);
        }
        
        #endregion
        
        #region POST
        
        public async Task<AddWeaponDetailResponse> AddWeaponDetailAsync(AddWeaponDetailCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
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
        
        public async Task<UpdateWeaponDetailByIdResponse> UpdateWeaponDetailByIdAsync(Guid id, UpdateWeaponDetailByIdCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
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
            
            return mapper.Map<UpdateWeaponDetailByIdResponse>(response);
        }
        
        #endregion
        
        #region Delete
        
        public async Task DeleteWeaponDetailByIdAsync(DeleteWeaponDetailByIdCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
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
