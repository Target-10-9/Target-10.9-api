using System.Security.Claims;
using AutoMapper;
using Target10._9.Business.Logs.Repositories;
using Target10._9.Business.ModeDetails.Commands;
using Target10._9.Business.ModeDetails.Queries;
using Target10._9.Business.ModeDetails.Repositories;
using Target10._9.Business.ModeDetails.Responses;
using Target10._9.Business.Services;

namespace Target10._9.Business.ModeDetails
{
    public interface IModeDetailsService
    {
        #region Get
        Task<List<GetModeDetailsResponse>> GetModeDetailsAsync(ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        Task<GetModeDetailByIdResponse> GetModeDetailByIdAsync(GetModeDetailByIdQuery query, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
        
        #region Post
        Task<AddModeDetailResponse> AddModeDetailAsync(AddModeDetailCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
        
        #region Put
        Task<UpdateModeDetailByIdResponse> UpdateModeDetailByIdAsync(Guid id, UpdateModeDetailByIdCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
        
        #region Delete
        Task DeleteModeDetailByIdAsync(DeleteModeDetailByIdCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
    }

    public class ModeDetailsService(
        IModeDetailsRepository modeDetailsRepository,
        ILogsRepository logsRepository,
        IValidationService validationService,
        IMapper mapper
        ) : IModeDetailsService
    {
        #region Get
        
        public async Task<List<GetModeDetailsResponse>> GetModeDetailsAsync(ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out _))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var modeDetails = await modeDetailsRepository.GetModeDetailsAsync(cancellationToken);

            return mapper.Map<List<GetModeDetailsResponse>>(modeDetails);
        }

        public async Task<GetModeDetailByIdResponse> GetModeDetailByIdAsync(GetModeDetailByIdQuery query, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(query, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out _))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var modeDetail = await modeDetailsRepository.GetModeDetailByIdAsync(query.Id, cancellationToken);
            
            return mapper.Map<GetModeDetailByIdResponse>(modeDetail);
        }

        #endregion
        
        #region Post
        
        public async Task<AddModeDetailResponse> AddModeDetailAsync(AddModeDetailCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(command, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");

            var modeDetail = await modeDetailsRepository.AddModeDetailAsync(
                command.ShootLimit,
                command.ShootingTime,
                command.RestTime,
                cancellationToken
            );
            
            await logsRepository.AddLogAsync(
                "ModeDetailAdded",
                $"Mode detail added with ID: {modeDetail.Id}",
                userIdGuid,
                cancellationToken
            );

            return mapper.Map<AddModeDetailResponse>(modeDetail);
        }
        
        #endregion
        
        #region Put
        
        public async Task<UpdateModeDetailByIdResponse> UpdateModeDetailByIdAsync(Guid id, UpdateModeDetailByIdCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(command, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");

            var modeDetail = await modeDetailsRepository.UpdateModeDetailByIdAsync(
                id,
                command.ShootLimit,
                command.ShootingTime,
                command.RestTime,
                cancellationToken
            );
            
            await logsRepository.AddLogAsync(
                "ModeDetailUpdated",
                $"Mode detail with ID: {id} updated",
                userIdGuid,
                cancellationToken
            );

            return mapper.Map<UpdateModeDetailByIdResponse>(modeDetail);
        }
        
        #endregion
        
        #region Delete
        
        public async Task DeleteModeDetailByIdAsync(DeleteModeDetailByIdCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(command, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");

            await modeDetailsRepository.DeleteModeDetailByIdAsync(command.Id, cancellationToken);
            
            await logsRepository.AddLogAsync(
                "ModeDetailDeleted",
                $"Mode detail with ID: {command.Id} deleted",
                userIdGuid,
                cancellationToken
            );
        }
        
        #endregion
    }
}
