using System.Security.Claims;
using Target10._9.Business.Services;
using Target10._9.Business.Users.Commands;
using Target10._9.Business.Users.Responses;

namespace Target10._9.Business.Users
{
    public interface IUsersService
    {
        #region Get

        Task<GetUserResponse> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
        
        #endregion

        #region Update

        Task<UpdateUserResponse> UpdateUserAsync(Guid userId, UpdateUserCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken);

        #endregion
    }

    public class UsersService(
        IValidationService validationService
        ) : IUsersService
    {

        public async Task<GetUserResponse> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            // Pour le test sans BDD, on crée une réponse fictive
            return new GetUserResponse
            {
                Id = userId,
                Email = "john.doe@gmail.com",
                FullName = "Test User"
            };
        }
        
        public async Task<UpdateUserResponse> UpdateUserAsync(Guid userId, UpdateUserCommand command, ClaimsPrincipal currentUser, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(command, cancellationToken);
            
            var userIdFromToken = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdFromToken == null || userId.ToString() != userIdFromToken)
                throw new UnauthorizedAccessException("You are not allowed to update this user");

            // Simuler l'utilisateur (à remplacer plus tard par un accès DB)
            var simulatedUser = new UpdateUserResponse
            {
                Id = userId,
                Email = command.Email,
                FullName = command.FullName
            };

            return simulatedUser;
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
