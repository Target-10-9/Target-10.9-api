using Target10._9.Business.Users.Entities;

namespace Target10._9.Business.Logs.Entities;

public class Log
{
    public Guid Id { get; set; }
    public string Action { get; set; } = null!;
    public string Details { get; set; } = null!;
    public DateTime DateLog { get; set; }
    public Guid UserId { get; set; }

    public User Users { get; set; } = null!;
}