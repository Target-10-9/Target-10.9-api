using Target10._9.Business.SessionModes.Entities;
using Target10._9.Business.WeaponDetails.Entities;

namespace Target10._9.Business.SessionModeWeaponDetails.Entities;

public class SessionModeWeaponDetail
{
    public Guid Id { get; set; }
    public Guid SessionModeId { get; set; }
    public Guid WeaponDetailsId { get; set; }
    
    public SessionMode SessionModes { get; set; } = null!;
    public WeaponDetail WeaponDetails { get; set; } = null!;
}