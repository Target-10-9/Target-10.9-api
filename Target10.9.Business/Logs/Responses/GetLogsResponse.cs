namespace Target10._9.Business.Logs.Responses;

public class GetLogsResponse
{
    public string Action { get; set; } = null!;
    public string Details { get; set; } = null!;
    public DateTime DateLog { get; set; }
    public Guid UserId { get; set; }
}