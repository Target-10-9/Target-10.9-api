namespace Target10._9.Business.WeaponDetails.Responses;

public class GetWeaponDetailResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string SerialNumber { get; set; } = null!;
}