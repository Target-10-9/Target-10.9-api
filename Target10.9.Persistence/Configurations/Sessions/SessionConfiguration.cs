using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target10._9.Business.Sessions.Entities;

namespace Target10._9.Persistence.Configurations.Sessions;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Users).WithMany(x => x.Sessions).HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.SessionModes).WithMany(x => x.Sessions).HasForeignKey(x => x.SessionModeId);
    }
}