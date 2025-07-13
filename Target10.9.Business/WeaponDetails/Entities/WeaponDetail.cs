using Target10._9.Business.SessionModeWeaponDetails.Entities;
using Target10._9.Business.Users.Entities;

namespace Target10._9.Business.WeaponDetails.Entities;

public class WeaponDetail
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string SerialNumber { get; set; } = null!;
    public Guid UserId { get; set; }
    
    public User Users { get; set; } = null!;
    public ICollection<SessionModeWeaponDetail> SessionModeWeaponDetails { get; set; } = new List<SessionModeWeaponDetail>();
}