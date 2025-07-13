using AutoMapper;
using Target10._9.Business.Authentications.Commands;
using Target10._9.Business.Authentications.Responses;
using Target10._9.Business.Providers;
using Target10._9.Business.Services;
using Target10._9.Business.Users.Repositories;

namespace Target10._9.Business.Authentications
{
    public interface IAuthenticationsService
    {
        Task<RegisterResponse> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken);
        Task<LoginResponse> LoginAsync(LoginCommand command, CancellationToken cancellationToken);
    }

    public class AuthenticationsService(
        IUsersRepository usersRepository,
        IJwtProvider jwtProvider,
        IValidationService validationService,
        IMapper mapper
    ) : IAuthenticationsService
    {

        public async Task<RegisterResponse> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken)
        {
            await validationService.ValidateAsync(command, cancellationToken);
            
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(command.Password);
            
            await usersRepository.AddUserAsync(
                command.Email,
                hashedPassword,
                command.FirstName,
                command.LastName,
                command.LicenseNumber,
                cancellationToken
            );
            
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
            
            var user = await usersRepository.GetUserByEmailAsync(command.Email, cancellationToken);
            
            if (!BCrypt.Net.BCrypt.Verify(command.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Email ou mot de passe incorrect");
            }
            
            var token = jwtProvider.GenerateToken(user.Id.ToString(), command.Email);

            var response = mapper.Map<LoginResponse>(user);
            response.Token = token;

            return response;
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
