using Target10._9.Business.ModeDetails.Entities;
using Target10._9.Business.SessionModeWeaponDetails.Entities;
using Target10._9.Business.Sessions.Entities;

namespace Target10._9.Business.SessionModes.Entities;

public class SessionMode
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public TimeOnly TimeLimits { get; set; }
    public TimeOnly WarmUp { get; set; }
    public string Discipline { get; set; } = null!;
    public Guid ModeDetailId { get; set; }
    
    public ModeDetail ModeDetails { get; set; } = null!;
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
    public ICollection<SessionModeWeaponDetail> SessionModeWeaponDetails { get; set; } = new List<SessionModeWeaponDetail>();
}