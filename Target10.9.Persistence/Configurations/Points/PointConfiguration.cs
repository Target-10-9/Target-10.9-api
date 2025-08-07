using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target10._9.Business.Points.Entities;

namespace Target10._9.Persistence.Configurations.Points;

public class PointConfiguration : IEntityTypeConfiguration<Point>
{
    public void Configure(EntityTypeBuilder<Point> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.X_Coordinate)
            .IsRequired();

        builder.Property(p => p.Y_Coordinate)
            .IsRequired();

        builder.Property(p => p.DateTimePoint)
            .IsRequired();
        
        builder.HasOne(p => p.Session)
            .WithMany(s => s.Points)
            .HasForeignKey(p => p.SessionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}