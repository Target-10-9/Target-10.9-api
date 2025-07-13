using Target10._9.Business.Services;

namespace Target10._9.Business.SessionModes
{
    public interface ISessionModesService
    {
    }

    public class SessionModesService(
        IValidationService validationService
        ) : ISessionModesService
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
