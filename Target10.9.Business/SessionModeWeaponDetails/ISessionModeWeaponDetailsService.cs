using Target10._9.Business.Services;

namespace Target10._9.Business.SessionModeWeaponDetails
{
    public interface ISessionModeWeaponDetailsService
    {
    }

    public class SessionModeWeaponDetailsService(
        IValidationService validationService
        ) : ISessionModeWeaponDetailsService
    {
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
