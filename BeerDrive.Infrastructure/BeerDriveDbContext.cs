using BeerDrive.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeerDrive.Infrastructure;

public class BeerDriveDbContext : DbContext
{
    public BeerDriveDbContext(DbContextOptions<BeerDriveDbContext> options)
        : base(options) { }

    public DbSet<FileEntry> FileEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BeerDriveDbContext).Assembly);
    }
}
