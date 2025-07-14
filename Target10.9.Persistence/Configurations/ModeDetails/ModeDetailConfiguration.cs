using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target10._9.Business.ModeDetails.Entities;

namespace Target10._9.Persistence.Configurations.ModeDetails;

public class ModeDetailConfiguration : IEntityTypeConfiguration<ModeDetail>
{
    public void Configure(EntityTypeBuilder<ModeDetail> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.SessionModes).WithOne(x => x.ModeDetails).HasForeignKey(x => x.ModeDetailId);
    }
}