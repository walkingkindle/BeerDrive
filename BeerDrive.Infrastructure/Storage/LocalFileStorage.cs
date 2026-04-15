using BeerDrive.Application.Abstractions.Storage;

namespace BeerDrive.Infrastructure.Storage;

public class LocalFileStorage : IFileStorage
{
    public async Task<string> SaveFileAsync(Stream stream, string fileName, string extension)
    {
        var path = Path.Combine("uploads", $"{Guid.NewGuid()}{extension}");

        using var fileStream = new FileStream(path, FileMode.Create);

        await stream.CopyToAsync(fileStream);

        return path;
    }
}
