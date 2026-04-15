using CSharpFunctionalExtensions;
using System.ComponentModel;

namespace BeerDrive.Domain.Entities;

public class FileSize : ValueObject<FileSize>
{
    public long SizeValue { get; set; }

    public FileSizeUnit Unit { get; set; } = default!;

    public static Result<FileSize> Create(long sizeValue, FileSizeUnit unit)
    {
        return Result.Success()
            .Ensure(() => sizeValue >= 0, "Value must be a valid numebr")
            .Ensure(() => Enum.IsDefined(typeof(FileSizeUnit), unit), "Unsupported file unit")
            .Map(() => new FileSize { SizeValue = sizeValue, Unit = unit });
    }

    protected override bool EqualsCore(FileSize other)
    {
        if(other == null) return false;

        if (other.GetType() != GetType())
            return false;

        return SizeValue == other.SizeValue && Unit == other.Unit;
    }

    protected override int GetHashCodeCore()
    {
        unchecked
        {
            return SizeValue.GetHashCode() ^ Unit.GetHashCode();
        }
    }
}
