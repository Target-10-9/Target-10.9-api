namespace Target10._9.Business.SessionModes.Responses;

public class GetSessionModesResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public TimeOnly TimeLimits { get; set; }
    public TimeOnly WarmUp { get; set; }
    public string Discipline { get; set; } = null!;
    public Guid ModeDetailId { get; set; }
}