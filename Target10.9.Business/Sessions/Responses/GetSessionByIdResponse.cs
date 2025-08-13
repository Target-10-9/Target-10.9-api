namespace Target10._9.Business.Sessions.Responses;

public class GetSessionByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public Guid UserId { get; set; }
    public SessionModeDto SessionModes { get; set; } = null!;
}

public class SessionModeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public TimeOnly TimeLimits { get; set; }
    public TimeOnly WarmUp { get; set; }
    public string Discipline { get; set; } = null!;
    public ModeDetailDto ModeDetails { get; set; } = null!;
    
    public List<SessionModeWeaponDetailDto> SessionModeWeaponDetails { get; set; } = [];
}

public class ModeDetailDto
{
    public Guid Id { get; set; }
    public int ShootLimit { get; set; }
    public TimeOnly ShootingTime { get; set; }
    public TimeOnly RestTime { get; set; }
}

public class SessionModeWeaponDetailDto
{
    public Guid Id { get; set; }
    public WeaponDetailDto WeaponDetails { get; set; } = null!;
}

public class WeaponDetailDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string SerialNumber { get; set; } = null!;
    public Guid UserId { get; set; }
}