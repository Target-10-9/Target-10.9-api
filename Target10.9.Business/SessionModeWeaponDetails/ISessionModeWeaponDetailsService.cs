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
}
