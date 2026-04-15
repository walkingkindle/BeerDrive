using BeerDrive.Application.Abstractions.Persistence;
using BeerDrive.Application.Abstractions.Storage;
using BeerDrive.Application.Models;
using BeerDrive.Domain.Entities;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace BeerDrive.Application;

public class FileEntryService(IFileStorage storage, IFileRepository repository, ILogger<FileEntryService> logger)
{
    public async Task<Result> UploadFile(UploadFileRequest request)
    {
        var stream = request.FileStream.OpenReadStream();

        var size = request.FileStream.Length;

        var result = FileEntry.Create(request.FileName, request.Extension, size, request.Unit, request.Type);

        if (result.IsFailure)
        {
            return result;
        }

        try
        {
            await storage.SaveFileAsync(stream, result.Value.Name.Name, result.Value.Name.Extension);

            await repository.AddAsync(result.Value);

            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to save a file, rolling back changes: {ex.Message}");

            throw;

        }
    }


}
