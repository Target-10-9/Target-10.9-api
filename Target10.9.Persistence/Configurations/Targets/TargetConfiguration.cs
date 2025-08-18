using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target10._9.Business.Targets.Entities;

namespace Target10._9.Persistence.Configurations.Targets;

public class TargetConfiguration : IEntityTypeConfiguration<Target>
{
    public void Configure(EntityTypeBuilder<Target> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.TargetId)
            .IsRequired();
        
        builder.Property(x => x.UserId)
            .IsRequired(false);
        
        builder.HasIndex(x => x.UserId)
            .IsUnique(false);
    }
}