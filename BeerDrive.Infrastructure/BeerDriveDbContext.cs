using BeerDrive.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeerDrive.Infrastructure;

public class BeerDriveDbContext : DbContext
{
    public DbSet<FileEntry> FileEntries => Set<FileEntry>();

    public BeerDriveDbContext(DbContextOptions<BeerDriveDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BeerDriveDbContext).Assembly);
    }

}
