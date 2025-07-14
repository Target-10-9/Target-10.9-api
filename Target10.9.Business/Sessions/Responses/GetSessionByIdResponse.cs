namespace Target10._9.Business.Sessions.Responses;

public class GetSessionByIdResponse
{
    public string Name { get; set; } = null!;
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public Guid UserId { get; set; }
    public Guid SessionModeId { get; set; }
}