using Microsoft.EntityFrameworkCore;

namespace DotNetMiddlewares;
internal sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<IdentityTracker> IdentityTrackers { set; get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var builder = modelBuilder.Entity<IdentityTracker>();

        builder.HasKey(it => it.Identity);
    }
}

internal record IdentityTracker(string Identity);
