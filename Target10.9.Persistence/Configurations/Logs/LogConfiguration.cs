using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target10._9.Business.Logs.Entities;

namespace Target10._9.Persistence.Configurations.Logs;

public class LogConfiguration : IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Users).WithMany(x => x.Logs).HasForeignKey(x => x.UserId);
    }
}