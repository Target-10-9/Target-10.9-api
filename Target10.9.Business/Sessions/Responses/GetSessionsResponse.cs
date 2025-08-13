namespace Target10._9.Business.Sessions.Responses;

public class GetSessionsResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
}