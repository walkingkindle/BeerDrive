using CSharpFunctionalExtensions;

namespace BeerDrive.Domain.Entities;

public sealed class FileEntry : Entity<int>
{
    public FileName Name { get; private set; } = default!;

    public FileType Type { get; private set; }

    public FileSize Size { get; private set; } = default!;

    public DateTimeOffset CreatedAt { get; private set; }

    public DateTimeOffset UpdatedAt { get; private set; }

    private FileEntry() { }

    public static Result<FileEntry> Create(FileName fileName, FileType fileType, FileSize fileSize)
    {
        return Result.Success()
        .Ensure(() => fileName != null, "File name must not be null")
        .Ensure(() => fileSize != null, "File size must not be null")
        .Ensure(() => Enum.IsDefined(typeof(FileType), fileType), "Invalid File Type")
        .Map(() => 
        {
            var now = DateTimeOffset.UtcNow;

            return new FileEntry
            {
                Name = fileName,
                Type = fileType,
                Size = fileSize,
                CreatedAt = now,
                UpdatedAt = now
            };
        });
    }
    public static Result<FileEntry> Create(string name, string extension, long size, FileSizeUnit unit, FileType type)
    {
        return FileName.Create(name, extension)
            .Bind(fileName =>
                FileSize.Create(size, unit)
                    .Bind(fileSize =>
                        Create(fileName, type, fileSize)
                    )
            );
    }

}
