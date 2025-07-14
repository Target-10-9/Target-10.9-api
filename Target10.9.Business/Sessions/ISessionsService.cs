using System.Security.Claims;
using AutoMapper;
using Target10._9.Business.Services;
using Target10._9.Business.Sessions.Commands;
using Target10._9.Business.Sessions.Queries;
using Target10._9.Business.Sessions.Repositories;
using Target10._9.Business.Sessions.Responses;

namespace Target10._9.Business.Sessions
{
    public interface ISessionsService
    {
        #region Get
        Task<List<GetSessionsResponse>> GetSessionsAsync(ClaimsPrincipal user, CancellationToken cancellationToken);
        Task<GetSessionByIdResponse> GetSessionByIdAsync(GetSessionByIdQuery query, ClaimsPrincipal user, CancellationToken cancellationToken);
        #endregion
        
        #region POST
        Task<AddSessionResponse> AddSessionAsync(AddSessionCommand command, ClaimsPrincipal user, CancellationToken cancellationToken);
        #endregion
        
        #region PUT
        Task<UpdateSessionByIdResponse> UpateSessionByIdAsync(Guid id, UpdateSessionByIdCommand command, ClaimsPrincipal user, CancellationToken cancellationToken);
        #endregion
    }

    public class SessionsService(
        ISessionsRepository sessionsRepository,
        IValidationService validationService,
        IMapper mapper
        ) : ISessionsService
    {
        #region Get

        public async Task<List<GetSessionsResponse>> GetSessionsAsync(ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var weapons = await sessionsRepository.GetSessionsAsync(userIdGuid, cancellationToken);

            return mapper.Map<List<GetSessionsResponse>>(weapons);
        }
        
        public async Task<GetSessionByIdResponse> GetSessionByIdAsync(GetSessionByIdQuery query, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(query, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var session = await sessionsRepository.GetSessionByIdAsync(query.Id, userIdGuid, cancellationToken);
            
            if (session == null)
                throw new KeyNotFoundException("Session not found.");

            return mapper.Map<GetSessionByIdResponse>(session);
        }

        #endregion
        
        #region POST
        
        public async Task<AddSessionResponse> AddSessionAsync(AddSessionCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(command, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");

            var sessionResponse = await sessionsRepository.AddSessionAsync(
                userIdGuid,
                command.Name,
                command.DateStart,
                command.DateEnd,
                command.SessionModeId,
                cancellationToken
            );

            return mapper.Map<AddSessionResponse>(sessionResponse);
        }
        
        #endregion
        
        #region PUT
        
        public async Task<UpdateSessionByIdResponse> UpateSessionByIdAsync(Guid id, UpdateSessionByIdCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(command, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");
            
            var session = await sessionsRepository.GetSessionByIdAsync(id, userIdGuid, cancellationToken);
            
            if (session == null)
                throw new KeyNotFoundException("Session not found.");

            await sessionsRepository.UpdateSessionByIdAsync(
                id,
                userIdGuid,
                command.Name,
                command.DateStart,
                command.DateEnd,
                command.SessionModeId,
                cancellationToken
            );

            return mapper.Map<UpdateSessionByIdResponse>(session);
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
