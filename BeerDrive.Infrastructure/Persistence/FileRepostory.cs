
using BeerDrive.Application.Abstractions.Persistence;
using BeerDrive.Domain.Entities;

namespace BeerDrive.Infrastructure.Persistence;

public class FileRepostory(BeerDriveDbContext Context): IFileRepository
{
    public async Task AddAsync(FileEntry file)
    {
        await Context.AddAsync(file);

        await Context.SaveChangesAsync();
    }
}
