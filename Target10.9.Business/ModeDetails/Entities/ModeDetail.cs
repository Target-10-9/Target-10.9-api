using Target10._9.Business.SessionModes.Entities;

namespace Target10._9.Business.ModeDetails.Entities;

public class ModeDetail
{
    public Guid Id { get; set; }
    public int ShootLimit { get; set; }
    public TimeOnly ShootingTime { get; set; }
    public TimeOnly RestTime { get; set; }
    
    public ICollection<SessionMode> SessionModes { get; set; } = new List<SessionMode>();
}