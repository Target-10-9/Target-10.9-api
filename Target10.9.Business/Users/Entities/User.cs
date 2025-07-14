using Target10._9.Business.Logs.Entities;
using Target10._9.Business.Sessions.Entities;
using Target10._9.Business.WeaponDetails.Entities;

namespace Target10._9.Business.Users.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string LicenseNumber { get; set; } = null!;
    
    public ICollection<WeaponDetail> Weapons { get; set; } = new List<WeaponDetail>();
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
    public ICollection<Log> Logs { get; set; } = new List<Log>();
}