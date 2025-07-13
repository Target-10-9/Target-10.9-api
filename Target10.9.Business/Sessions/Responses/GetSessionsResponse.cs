namespace Target10._9.Business.Sessions.Responses;

public class GetSessionsResponse
{
    public string Name { get; set; } = null!;
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
}