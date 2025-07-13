using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target10._9.Business.SessionModeWeaponDetails.Entities;

namespace Target10._9.Persistence.Configurations.SessionModeWeaponDetails;

public class SessionModeWeaponDetailConfiguration : IEntityTypeConfiguration<SessionModeWeaponDetail>
{
    public void Configure(EntityTypeBuilder<SessionModeWeaponDetail> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.SessionModes).WithMany(x => x.SessionModeWeaponDetails).HasForeignKey(x => x.SessionModeId);
        builder.HasOne(x => x.WeaponDetails).WithMany(x => x.SessionModeWeaponDetails).HasForeignKey(x => x.WeaponDetailsId);
    }
}