namespace Target10._9.Business.ModeDetails.Responses;

public class UpdateModeDetailByIdResponse
{
    public int ShootLimit { get; set; }
    public TimeOnly ShootingTime { get; set; }
    public TimeOnly RestTime { get; set; }
}