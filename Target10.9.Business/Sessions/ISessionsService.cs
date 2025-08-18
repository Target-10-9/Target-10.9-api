using System.Security.Claims;
using AutoMapper;
using Target10._9.Business.Common.Exceptions;
using Target10._9.Business.Logs.Repositories;
using Target10._9.Business.Services;
using Target10._9.Business.Sessions.Commands;
using Target10._9.Business.Sessions.Queries;
using Target10._9.Business.Sessions.Repositories;
using Target10._9.Business.Sessions.Responses;
using Target10._9.Business.Targets.Repositories;

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
        
        #region DELETE
        Task DeleteSessionByIdAsync(DeleteSessionByIdCommand command, ClaimsPrincipal user, CancellationToken cancellationToken);
        #endregion
    }

    public class SessionsService(
        ISessionsRepository sessionsRepository,
        ITargetsRepository targetsRepository,
        ILogsRepository logsRepository,
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
            
            var sessions = await sessionsRepository.GetSessionsAsync(userIdGuid, cancellationToken);

            return mapper.Map<List<GetSessionsResponse>>(sessions);
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
            
            await logsRepository.AddLogAsync(
                "Session Created",
                $"Session '{command.Name}' created by user {userIdGuid}.",
                userIdGuid,
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
            
            switch (command.Etat)
            {
                case SessionEtat.InProgress:
                    var alreadyInProgress = await sessionsRepository
                        .CheckIfSessionEtatInProgressExistAsync(userIdGuid, cancellationToken);

                    if (alreadyInProgress)
                        throw new BusinessRuleException("You already have a session in progress.");
        
                    var targetUserAlreadyExist = await targetsRepository
                        .CheckIfTargetUserExistAsync(command.TargetId, userIdGuid, cancellationToken);

                    if (targetUserAlreadyExist)
                        throw new BusinessRuleException("Target user already exists in this session.");

                    await targetsRepository
                        .AddTargetUserAsync(command.TargetId, userIdGuid, cancellationToken);
                    break;

                case SessionEtat.Finished:
                    await targetsRepository
                        .DeleteTargetUserAsync(command.TargetId, userIdGuid, cancellationToken);
                    break;
            }

            await sessionsRepository.UpdateSessionByIdAsync(
                id,
                userIdGuid,
                command.Name,
                command.DateStart,
                command.DateEnd,
                command.SessionModeId,
                command.Etat,
                cancellationToken
            );
            
            await logsRepository.AddLogAsync(
                "Session Updated",
                $"Session '{command.Name}' updated by user {userIdGuid}.",
                userIdGuid,
                cancellationToken
            );

            return mapper.Map<UpdateSessionByIdResponse>(session);
        }
        
        #endregion
        
        #region DELETE
        
        public async Task DeleteSessionByIdAsync(DeleteSessionByIdCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(command, cancellationToken);
            
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!Guid.TryParse(userId, out var userIdGuid))
                throw new UnauthorizedAccessException("Invalid user identifier.");

            await sessionsRepository.DeleteSessionByIdAsync(command.Id, userIdGuid, cancellationToken);
            
            await logsRepository.AddLogAsync(
                "Session Deleted",
                $"Session with ID {command.Id} deleted by user {userIdGuid}.",
                userIdGuid,
                cancellationToken
            );
        }
        
        #endregion
    }
}
