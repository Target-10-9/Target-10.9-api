using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target10._9.Business.SessionModes.Entities;

namespace Target10._9.Persistence.Configurations.SessionModes;

public class SessionModeConfiguration : IEntityTypeConfiguration<SessionMode>
{
    public void Configure(EntityTypeBuilder<SessionMode> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.ModeDetails).WithMany(x => x.SessionModes).HasForeignKey(x => x.ModeDetailId);
        builder.HasMany(x => x.Sessions).WithOne(x => x.SessionModes).HasForeignKey(x => x.SessionModeId);
        builder.HasMany(x => x.SessionModeWeaponDetails).WithOne(x => x.SessionModes).HasForeignKey(x => x.SessionModeId);
    }
}