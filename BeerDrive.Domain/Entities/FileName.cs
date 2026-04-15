using CSharpFunctionalExtensions;

namespace BeerDrive.Domain.Entities;
public sealed class FileName : ValueObject<FileName>
{
    public string Name { get; private set; } = default!;

    public string Extension { get; private set; } = default!;

    private FileName() { }
    private FileName(string name, string extension)
    {
        Name = name;
        Extension = extension;
    }
    public static Result<FileName> Create(string name, string extension)
    {
        return Result.Success()
            .Ensure(() => !string.IsNullOrWhiteSpace(name), "Name cannot be empty")
            .Ensure(() => !string.IsNullOrWhiteSpace(extension), "Extension cannot be empty")
            .Ensure(() => FileHelpers.SupportedExtensions.Contains(extension), "File extension not supported")
            .Map(() => new FileName(name, extension));
    }
    protected override bool EqualsCore(FileName other)
    {
        if(other == null) return false;

        if(other.GetType() != GetType()) return false;

        return Name.GetHashCode() == other.Name.GetHashCode() 
            && Extension.GetHashCode() == other.Extension.GetHashCode();
    }

    protected override int GetHashCodeCore()
    {
        return HashCode.Combine(Name, Extension);
    }
}
