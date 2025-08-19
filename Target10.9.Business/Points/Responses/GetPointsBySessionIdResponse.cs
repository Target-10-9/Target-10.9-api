namespace Target10._9.Business.Points.Responses;

public class GetPointsBySessionIdResponse
{
    public Guid Id { get; set; }
    public float X_Coordinate { get; set; }
    public float Y_Coordinate { get; set; }
    public DateTime DateTimePoint { get; set; }
    
    public Guid SessionId { get; set; }
}