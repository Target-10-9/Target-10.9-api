using System.Security.Claims;
using AutoMapper;
using Target10._9.Business.Logs.Repositories;
using Target10._9.Business.Services;
using Target10._9.Business.Users.Commands;
using Target10._9.Business.Users.Queries;
using Target10._9.Business.Users.Repositories;
using Target10._9.Business.Users.Responses;

namespace Target10._9.Business.Users
{
    public interface IUsersService
    {
        #region Get
        Task<GetUserByIdResponse> GetUserByIdAsync(GetUserByIdQuery byIdQuery, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion

        #region Update
        Task<UpdateUserByIdResponse> UpdateUserAsync(Guid id, UpdateUserByIdCommand byIdCommand, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion

        #region Delete
        Task DeleteUserAsync(DeleteUserByIdCommand byIdCommand, ClaimsPrincipal currentUser, CancellationToken cancellationToken);
        #endregion
    }

    public class UsersService(
        IUsersRepository usersRepository,
        ILogsRepository logsRepository,
        IValidationService validationService,
        IMapper mapper
        ) : IUsersService
    {

        #region Get
        
        public async Task<GetUserByIdResponse> GetUserByIdAsync(GetUserByIdQuery query, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(query, cancellationToken);
            
            var userIdFromToken = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdFromToken == null || query.Id.ToString() != userIdFromToken)
                throw new UnauthorizedAccessException("You are not allowed to get this user");
            
            var user = await usersRepository.GetUserByIdAsync(query.Id, cancellationToken);

            return mapper.Map<GetUserByIdResponse>(user);
        }
        
        #endregion
        
        #region Put
        
        public async Task<UpdateUserByIdResponse> UpdateUserAsync(Guid id, UpdateUserByIdCommand byIdCommand, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(byIdCommand, cancellationToken);
            
            var userIdFromToken = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdFromToken == null || id.ToString() != userIdFromToken)
                throw new UnauthorizedAccessException("You are not allowed to update this user");
            
            var user = await usersRepository.UpdateUserAsync(
                id,
                byIdCommand.Email,
                byIdCommand.FirstName,
                byIdCommand.LastName,
                byIdCommand.LicenseNumber,
                cancellationToken
            );
            
            await logsRepository.AddLogAsync(
                "Update User",
                $"User {id} updated",
                id,
                cancellationToken
            );

            return mapper.Map<UpdateUserByIdResponse>(user);
        }
        
        #endregion
        
        #region Delete
        
        public async Task DeleteUserAsync(DeleteUserByIdCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            var userIdFromToken = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdFromToken == null || command.Id.ToString() != userIdFromToken)
                throw new UnauthorizedAccessException("You are not allowed to update this user");

            await usersRepository.DeleteUserByIdAsync(command.Id, cancellationToken);
            
            await logsRepository.AddLogAsync(
                "Delete User",
                $"User {command.Id} deleted",
                command.Id,
                cancellationToken
            );
        }
        
        #endregion
    }
}
