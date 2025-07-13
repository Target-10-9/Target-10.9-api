using Target10._9.Business.SessionModes.Entities;
using Target10._9.Business.Users.Entities;

namespace Target10._9.Business.Sessions.Entities;

public class Session
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public Guid UserId { get; set; }
    public Guid SessionModeId { get; set; }
    
    public User Users { get; set; } = null!;
    public SessionMode SessionModes { get; set; } = null!;
}