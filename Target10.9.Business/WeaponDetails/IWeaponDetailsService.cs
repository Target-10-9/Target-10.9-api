using System.Security.Claims;
using AutoMapper;
using Target10._9.Business.Logs.Repositories;
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
        Task<GetWeaponDetailByIdResponse> GetWeaponDetailByIdAsync(GetWeaponDetailByIdQuery query, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
        
        #region Post
        Task<AddWeaponDetailResponse> AddWeaponDetailAsync(AddWeaponDetailCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
        
        #region Put
        Task<UpdateWeaponDetailByIdResponse> UpdateWeaponDetailByIdAsync(Guid id, UpdateWeaponDetailByIdCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
        
        #region Delete
        Task DeleteWeaponDetailByIdAsync(DeleteWeaponDetailByIdCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
    }

    public class WeaponDetailsService(
        IWeaponDetailsRepository weaponDetailsRepository,
        ILogsRepository logsRepository,
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
        
        public async Task<GetWeaponDetailByIdResponse> GetWeaponDetailByIdAsync(GetWeaponDetailByIdQuery query,  ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(query, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");

            var weapon = await weaponDetailsRepository.GetWeaponDetailByIdAsync(query.Id, userIdGuid, cancellationToken);

            return mapper.Map<GetWeaponDetailByIdResponse>(weapon);
        }
        
        #endregion
        
        #region Post
        
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
            
            await logsRepository.AddLogAsync(
                "AddWeaponDetail",
                $"Weapon detail with ID {response.Id} added by user {userIdGuid}.",
                userIdGuid,
                cancellationToken
            );
            
            return mapper.Map<AddWeaponDetailResponse>(response);
        }
        
        #endregion
        
        #region Put
        
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
            
            await logsRepository.AddLogAsync(
                "UpdateWeaponDetail",
                $"Weapon detail with ID {id} updated by user {userIdGuid}.",
                userIdGuid,
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
            
            await logsRepository.AddLogAsync(
                "DeleteWeaponDetail",
                $"Weapon detail with ID {command.Id} deleted by user {userIdGuid}.",
                userIdGuid,
                cancellationToken
            );
        }
        
        #endregion
    }
}
