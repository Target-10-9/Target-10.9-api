namespace Target10._9.Business.Points.Responses;

public class GetPointsResponse
{
    public Guid Id { get; set; }
    public float X_Coordinate { get; set; }
    public float Y_Coordinate { get; set; }
    public DateTime DateTimePoint { get; set; }
}