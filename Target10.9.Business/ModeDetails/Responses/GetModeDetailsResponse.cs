namespace Target10._9.Business.ModeDetails.Responses;

public class GetModeDetailsResponse
{
    public Guid Id { get; set; }
    public int ShootLimit { get; set; }
    public TimeOnly ShootingTime { get; set; }
    public TimeOnly RestTime { get; set; }
}