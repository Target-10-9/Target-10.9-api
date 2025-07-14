using System.Security.Claims;
using Target10._9.Business.Services;
using Target10._9.Business.Users;

namespace Target10._9.Business.WeaponDetails
{
    public interface IWeaponDetailsService
    {
    }

    public class WeaponDetailsService(
        IValidationService validationService
        ) : IWeaponDetailsService
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
