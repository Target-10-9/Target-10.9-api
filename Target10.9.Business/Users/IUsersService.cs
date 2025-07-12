using Target10._9.Business.Users.Responses;

namespace Target10._9.Business.Users
{
    public interface IUsersService
    {
        Task<GetUserResponse> GetUserByIdAsync(Guid userId);
    }

    public class UsersService() : IUsersService
    {

        public async Task<GetUserResponse> GetUserByIdAsync(Guid userId)
        {
            // Pour le test sans BDD, on crée une réponse fictive
            return new GetUserResponse
            {
                Id = userId,
                Email = "john.doe@gmail.com",
                FullName = "Test User"
            };
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
