namespace Target10._9.Business.ModeDetails.Responses;

public class GetModeDetailByIdResponse
{
    public int ShootLimit { get; set; }
    public TimeOnly ShootingTime { get; set; }
    public TimeOnly RestTime { get; set; }
}