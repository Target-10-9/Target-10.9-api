using Target10._9.Business.Sessions.Entities;

namespace Target10._9.Business.Points.Entities;

public class Point
{
    public Guid Id { get; set; }
    public float X_Coordinate { get; set; }
    public float Y_Coordinate { get; set; }
    public DateTime DateTimePoint { get; set; }
    
    public Guid SessionId { get; set; }
    
    public Session Session { get; set; }
}