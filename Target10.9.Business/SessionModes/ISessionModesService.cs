using System.Security.Claims;
using AutoMapper;
using Target10._9.Business.Services;
using Target10._9.Business.SessionModes.Commands;
using Target10._9.Business.SessionModes.Queries;
using Target10._9.Business.SessionModes.Repositories;
using Target10._9.Business.SessionModes.Responses;

namespace Target10._9.Business.SessionModes
{
    public interface ISessionModesService
    {
        #region Get
        Task<List<GetSessionModesResponse>> GetSessionModesAsync(ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        Task<GetSessionModeByIdResponse> GetSessionModeByIdAsync(GetSessionModeByIdQuery query, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
        
        #region Post
        Task<AddSessionModeResponse> AddSessionModeAsync(AddSessionModeCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
        
        #region Put
        Task<UpdateSessionModeByIdResponse> UpdateSessionModeByIdAsync(Guid id, UpdateSessionModeByIdCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
    }

    public class SessionModesService(
        ISessionModesRepository sessionModesRepository,
        IValidationService validationService,
        IMapper mapper
        ) : ISessionModesService
    {
        #region Get
        
        public async Task<List<GetSessionModesResponse>> GetSessionModesAsync(ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var sessionModes = await sessionModesRepository.GetSessionModesAsync(cancellationToken);

            return mapper.Map<List<GetSessionModesResponse>>(sessionModes);
        }
        
        public async Task<GetSessionModeByIdResponse> GetSessionModeByIdAsync(GetSessionModeByIdQuery query, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(query, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var sessionMode = await sessionModesRepository.GetSessionModeByIdAsync(query.Id, cancellationToken);
            
            return mapper.Map<GetSessionModeByIdResponse>(sessionMode);
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

            return mapper.Map<AddSessionModeResponse>(sessionMode);
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

            return mapper.Map<UpdateSessionModeByIdResponse>(response);
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
