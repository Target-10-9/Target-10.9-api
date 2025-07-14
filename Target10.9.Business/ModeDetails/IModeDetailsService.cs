using System.Security.Claims;
using AutoMapper;
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
    }

    public class ModeDetailsService(
        IModeDetailsRepository modeDetailsRepository,
        IValidationService validationService,
        IMapper mapper
        ) : IModeDetailsService
    {
        #region Get
        
        public async Task<List<GetModeDetailsResponse>> GetModeDetailsAsync(ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var modeDetails = await modeDetailsRepository.GetModeDetailsAsync(cancellationToken);

            return mapper.Map<List<GetModeDetailsResponse>>(modeDetails);
        }

        public async Task<GetModeDetailByIdResponse> GetModeDetailByIdAsync(GetModeDetailByIdQuery query, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(query, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var modeDetail = await modeDetailsRepository.GetModeDetailByIdAsync(query.Id, cancellationToken);
            
            return mapper.Map<GetModeDetailByIdResponse>(modeDetail);
        }

        #endregion
    }
}
