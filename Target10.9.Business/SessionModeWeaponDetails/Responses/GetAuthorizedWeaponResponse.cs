namespace Target10._9.Business.SessionModeWeaponDetails.Responses;

public class GetAuthorizedWeaponResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string SerialNumber { get; set; } = null!;
    public Guid UserId { get; set; }
}