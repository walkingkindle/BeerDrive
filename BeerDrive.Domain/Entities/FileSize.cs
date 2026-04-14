using CSharpFunctionalExtensions;
using System.ComponentModel;

namespace BeerDrive.Domain.Entities
{
    public class FileSize : ValueObject<FileSize>
    {
        public int SizeValue { get; set; }

        public string Unit { get; set; } = default!;

        public static Result<FileSize> Create(int sizeValue, string unit)
        {
            return Result.Success()
                .Ensure(() => sizeValue >= 0, "Value must be a valid numebr")
                .Ensure(() => !string.IsNullOrEmpty(unit), "Unit must be a valid value")
                .Ensure(() => FileHelpers.SupportedFileUnits.Contains(unit), "Unsupported file unit")
                .Map(() => new FileSize { SizeValue = sizeValue, Unit = unit });
        }

        protected override bool EqualsCore(FileSize other)
        {
            if(other == null) return false;

            if(other.GetType() != this.GetType()) return false;

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
}