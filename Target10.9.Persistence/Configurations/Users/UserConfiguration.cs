using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Target10._9.Business.Users.Entities;

namespace Target10._9.Persistence.Configurations.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Weapons).WithOne(x => x.Users).HasForeignKey(x => x.UserId);
        builder.HasMany(x => x.Sessions).WithOne(x => x.Users).HasForeignKey(x => x.UserId);
        builder.HasMany(x => x.Logs).WithOne(x => x.Users).HasForeignKey(x => x.UserId);
    }
}