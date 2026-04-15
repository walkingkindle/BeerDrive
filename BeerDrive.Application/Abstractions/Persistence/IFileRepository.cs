using BeerDrive.Domain.Entities;

namespace BeerDrive.Application.Abstractions.Persistence;

public interface IFileRepository
{
    Task AddAsync(FileEntry file);
}
