namespace Target10._9.Business.Users.Responses;

public class GetUserResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string LicenseNumber { get; set; } = null!;
}