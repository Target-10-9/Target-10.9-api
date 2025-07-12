using Microsoft.EntityFrameworkCore;
using Target10._9.Business.Accounts.Entities;

namespace Target10._9.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    #region DbSets
    
    public virtual DbSet<User> Users { get; set; }
    
    #endregion DbSets
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}