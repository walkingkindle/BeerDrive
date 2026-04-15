namespace BeerDrive.Application.Abstractions.Storage;

public interface IFileStorage
{
    Task<string> SaveFileAsync(Stream fileStream, string fileName, string extension);

}
