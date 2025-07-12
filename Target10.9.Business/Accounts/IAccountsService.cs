using AutoMapper;
using Microsoft.Extensions.Configuration;
using Target10._9.Business.Accounts.Commands;
using Target10._9.Business.Accounts.Repositories;
using Target10._9.Business.Accounts.Responses;
using Target10._9.Business.Providers;
using Target10._9.Business.Services;

namespace Target10._9.Business.Accounts
{
    public interface IAccountsService
    {
        Task<RegisterResponse> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken);
        Task<LoginResponse> LoginAsync(LoginCommand command, CancellationToken cancellationToken);
    }

    public class AccountsService(
        IUsersRepository usersRepository,
        IJwtProvider jwtProvider,
        IValidationService validationService,
        IConfiguration configuration,
        IMapper mapper
    ) : IAccountsService
    {

        public async Task<RegisterResponse> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(command, cancellationToken);
            
            // if (await usersRepository.CheckIfEmailExistsAsync(command.Email, cancellationToken))
            // {
            //     throw new InvalidOperationException("Cet email est déjà utilisé");
            // }
            //
            //  var user = new User
            // {
            //     Email = command.Email,
            //     Password = command.Password,
            //     FirstName = command.FirstName,
            //     LastName = command.LastName,
            //     LicenseNumber = command.LicenseNumber
            // };
            //
            // await usersRepository.AddUserAsync(user, cancellationToken);

            // Pour le test sans BDD, on crée une réponse fictive
            return new RegisterResponse
            {
                Email = command.Email,
                FullName = command.FirstName + " " + command.LastName,
                Message = "Utilisateur inscrit avec succès"
            };
        }

        public async Task<LoginResponse> LoginAsync(LoginCommand command, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(command, cancellationToken);
    
            var userId = Guid.NewGuid().ToString();
            var token = jwtProvider.GenerateToken(command.Email, userId);

            return new LoginResponse
            { 
                UserId = userId,
                Email = command.Email,
                Token = token
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
