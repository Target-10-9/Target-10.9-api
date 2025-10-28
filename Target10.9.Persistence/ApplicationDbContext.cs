using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Target10._9.Business.Logs.Entities;
using Target10._9.Business.ModeDetails.Entities;
using Target10._9.Business.Points.Entities;
using Target10._9.Business.SessionModes.Entities;
using Target10._9.Business.SessionModeWeaponDetails.Entities;
using Target10._9.Business.Sessions.Entities;
using Target10._9.Business.TargetReferences.Entities;
using Target10._9.Business.Targets.Entities;
using Target10._9.Business.Users.Entities;
using Target10._9.Business.WeaponDetails.Entities;

namespace Target10._9.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly IDbConfiguration _config = null!;

    public ApplicationDbContext() { }

    public ApplicationDbContext(IDbConfiguration config)
    {
        _config = config;
    }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    #region DbSets
    
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Log> Logs { get; set; }
    public virtual DbSet<Session> Sessions { get; set; }
    public virtual DbSet<SessionMode> SessionModes { get; set; }
    public virtual DbSet<ModeDetail> ModeDetails { get; set; }
    public virtual DbSet<SessionModeWeaponDetail> SessionModeWeaponDetails { get; set; }
    public virtual DbSet<WeaponDetail> WeaponDetails { get; set; }
    public virtual DbSet<Point> Points { get; set; }
    
    public virtual DbSet<Target> Targets { get; set; }
    public virtual DbSet<TargetReference> TargetReferences { get; set; }
    
    #endregion DbSets
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_config != null)
        {
            optionsBuilder.UseNpgsql(_config.ConnectionString);
        }

        base.OnConfiguring(optionsBuilder);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Assembly assembly = typeof(ApplicationDbContext).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}