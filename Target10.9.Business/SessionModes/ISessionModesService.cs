using System.Security.Claims;
using AutoMapper;
using Target10._9.Business.Logs.Repositories;
using Target10._9.Business.Services;
using Target10._9.Business.SessionModes.Commands;
using Target10._9.Business.SessionModes.Queries;
using Target10._9.Business.SessionModes.Repositories;
using Target10._9.Business.SessionModes.Responses;
using Target10._9.Business.SessionModeWeaponDetails.Commands;
using Target10._9.Business.SessionModeWeaponDetails.Entities;
using Target10._9.Business.SessionModeWeaponDetails.Queries;
using Target10._9.Business.SessionModeWeaponDetails.Responses;

namespace Target10._9.Business.SessionModes
{
    public interface ISessionModesService
    {
        #region Get
        Task<List<GetSessionModesResponse>> GetSessionModesAsync(ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        Task<GetSessionModeByIdResponse> GetSessionModeByIdAsync(GetSessionModeByIdQuery query, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        Task<List<GetAuthorizedWeaponResponse>> GetAuthorizedWeaponsAsync(GetAuthorizedWeaponsQuery query, CancellationToken cancellationToken);
        #endregion
        
        #region Post
        Task<AddSessionModeResponse> AddSessionModeAsync(AddSessionModeCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        Task AddAuthorizedWeaponAsync(Guid sessionModeId, AddAuthorizedWeaponCommand command,
            CancellationToken cancellationToken);
        #endregion
        
        #region Put
        Task<UpdateSessionModeByIdResponse> UpdateSessionModeByIdAsync(Guid id, UpdateSessionModeByIdCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
        
        #region Delete
        Task DeleteSessionModeByIdAsync(DeleteSessionModeByIdCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);

        Task RemoveAuthorizedWeaponAsync(Guid sessionModeId, Guid weaponDetailsId,
            CancellationToken cancellationToken);
        #endregion
    }

    public class SessionModesService(
        ISessionModesRepository sessionModesRepository,
        ILogsRepository logsRepository,
        IValidationService validationService,
        IMapper mapper
        ) : ISessionModesService
    {
        #region Get
        
        public async Task<List<GetSessionModesResponse>> GetSessionModesAsync(ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out _))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var sessionModes = await sessionModesRepository.GetSessionModesAsync(cancellationToken);

            return mapper.Map<List<GetSessionModesResponse>>(sessionModes);
        }
        
        public async Task<GetSessionModeByIdResponse> GetSessionModeByIdAsync(GetSessionModeByIdQuery query, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(query, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out _))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var sessionMode = await sessionModesRepository.GetSessionModeByIdAsync(query.Id, cancellationToken);
            
            return mapper.Map<GetSessionModeByIdResponse>(sessionMode);
        }
        
        public async Task<List<GetAuthorizedWeaponResponse>> GetAuthorizedWeaponsAsync(GetAuthorizedWeaponsQuery query, CancellationToken cancellationToken)
        {
            var weapons = await sessionModesRepository.GetAuthorizedWeaponsAsync(query.Id, cancellationToken);
            return mapper.Map<List<GetAuthorizedWeaponResponse>>(weapons);
        }

        #endregion
        
        #region Post
        
        public async Task<AddSessionModeResponse> AddSessionModeAsync(AddSessionModeCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(command, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");

            var sessionMode = await sessionModesRepository.AddSessionModeAsync(
                command.Name,
                command.TimeLimits,
                command.WarmUp,
                command.Discipline,
                command.ModeDetailId,
                cancellationToken
            );
            
            await logsRepository.AddLogAsync(
                "AddSessionMode",
                $"Added session mode with ID: {sessionMode.Id}",
                userIdGuid,
                cancellationToken
            );

            return mapper.Map<AddSessionModeResponse>(sessionMode);
        }
        
        public async Task AddAuthorizedWeaponAsync(Guid sessionModeId, AddAuthorizedWeaponCommand command, CancellationToken cancellationToken)
        {
            await sessionModesRepository.AddAuthorizedWeaponAsync(
                sessionModeId,
                command.WeaponDetailId,
                cancellationToken
            );
        }
        
        #endregion
        
        #region Put
        
        public async Task<UpdateSessionModeByIdResponse> UpdateSessionModeByIdAsync(Guid id, UpdateSessionModeByIdCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(command, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var response = await sessionModesRepository.UpdateSessionModeAsync(
                id,
                command.Name,
                command.TimeLimits,
                command.WarmUp,
                command.Discipline,
                command.ModeDetailId,
                cancellationToken
            );
            
            await logsRepository.AddLogAsync(
                "UpdateSessionMode",
                $"Updated session mode with ID: {id}",
                userIdGuid,
                cancellationToken
            );

            return mapper.Map<UpdateSessionModeByIdResponse>(response);
        }
        
        #endregion
        
        #region Delete
        
        public async Task DeleteSessionModeByIdAsync(DeleteSessionModeByIdCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(command, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            await sessionModesRepository.DeleteSessionModeByIdAsync(command.Id, cancellationToken);
            
            await logsRepository.AddLogAsync(
                "DeleteSessionMode",
                $"Deleted session mode with ID: {command.Id}",
                userIdGuid,
                cancellationToken
            );
        }
        
        public async Task RemoveAuthorizedWeaponAsync(Guid sessionModeId, Guid weaponDetailsId, CancellationToken cancellationToken)
        {
            await sessionModesRepository.RemoveAuthorizedWeaponAsync(sessionModeId, weaponDetailsId, cancellationToken);
        }
        
        #endregion
    }
}
