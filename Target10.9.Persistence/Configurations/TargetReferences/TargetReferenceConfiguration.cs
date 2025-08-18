using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target10._9.Business.TargetReferences.Entities;
using Target10._9.Business.Targets.Entities;

namespace Target10._9.Persistence.Configurations.TargetReferences;

public class TargetConfiguration : IEntityTypeConfiguration<TargetReference>
{
    public void Configure(EntityTypeBuilder<TargetReference> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.TargetReferenceId)
            .IsRequired();
    }
}