using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target10._9.Business.WeaponDetails.Entities;

namespace Target10._9.Persistence.Configurations.WeaponDetails;

public class WeaponDetailConfiguration : IEntityTypeConfiguration<WeaponDetail>
{
    public void Configure(EntityTypeBuilder<WeaponDetail> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Users).WithMany(x => x.Weapons).HasForeignKey(x => x.UserId);
        builder.HasMany(x => x.SessionModeWeaponDetails).WithOne(x => x.WeaponDetails).HasForeignKey(x => x.WeaponDetailsId);
    }
}