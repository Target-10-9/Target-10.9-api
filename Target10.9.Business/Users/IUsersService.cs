using System.Security.Claims;
using AutoMapper;
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
        IValidationService validationService,
        IMapper mapper
        ) : IUsersService
    {

        public async Task<GetUserByIdResponse> GetUserByIdAsync(GetUserByIdQuery query, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(query, cancellationToken);
            
            var userIdFromToken = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdFromToken == null || query.Id.ToString() != userIdFromToken)
                throw new UnauthorizedAccessException("You are not allowed to get this user");
            
            var user = await usersRepository.GetUserByIdAsync(query.Id, cancellationToken);

            return mapper.Map<GetUserByIdResponse>(user);
        }
        
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

            return mapper.Map<UpdateUserByIdResponse>(user);
        }
        
        public async Task DeleteUserAsync(DeleteUserByIdCommand byIdCommand, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            var userIdFromToken = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdFromToken == null || byIdCommand.Id.ToString() != userIdFromToken)
                throw new UnauthorizedAccessException("You are not allowed to update this user");

            await usersRepository.DeleteUserByIdAsync(byIdCommand.Id, cancellationToken);
        }
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
