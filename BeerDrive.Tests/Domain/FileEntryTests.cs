using Xunit;
using FluentAssertions;
using BeerDrive.Domain.Entities;

namespace BeerDrive.Tests.Domain;

public class FileEntry_Tests
{
    [Fact]
    public void Create_Should_Succeed_With_Valid_ValueObjects()
    {
        var fileName = FileName.Create("file", "png").Value;
        var fileSize = FileSize.Create(100, FileSizeUnit.MB).Value;

        var result = FileEntry.Create(fileName, FileType.Image, fileSize);

        result.IsSuccess.Should().BeTrue();

        var entity = result.Value;

        entity.Name.Should().Be(fileName);
        entity.Size.Should().Be(fileSize);
        entity.Type.Should().Be(FileType.Image);
        entity.CreatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        entity.UpdatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Create_Should_Fail_When_FileName_Is_Null()
    {
        var fileSize = FileSize.Create(100, FileSizeUnit.MB).Value;

        var result = FileEntry.Create(null!, FileType.Image, fileSize);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("File name must not be null");
    }

    [Fact]
    public void Create_Should_Fail_When_FileSize_Is_Null()
    {
        var fileName = FileName.Create("file", "png").Value;

        var result = FileEntry.Create(fileName, FileType.Image, null!);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("File size must not be null");
    }

    [Fact]
    public void Create_Should_Fail_When_FileType_Is_Invalid()
    {
        var fileName = FileName.Create("file", "png").Value;
        var fileSize = FileSize.Create(100, FileSizeUnit.MB).Value;

        var result = FileEntry.Create(fileName, (FileType)999, fileSize);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Invalid File Type");
    }

    [Fact]
    public void Create_With_Primitives_Should_Succeed()
    {
        var result = FileEntry.Create("file", "png", 100, FileSizeUnit.MB, FileType.Image);

        result.IsSuccess.Should().BeTrue();

        var entity = result.Value;

        entity.Name.Name.Should().Be("file");
        entity.Name.Extension.Should().Be("png");
        entity.Size.SizeValue.Should().Be(100);
        entity.Size.Unit.Should().Be(FileSizeUnit.MB);
    }

    [Fact]
    public void Create_With_Primitives_Should_Fail_When_FileName_Invalid()
    {
        var result = FileEntry.Create("", "png", 100, FileSizeUnit.MB, FileType.Image);

        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void Create_With_Primitives_Should_Fail_When_FileSize_Invalid()
    {
        var result = FileEntry.Create("file", "png", -1, FileSizeUnit.MB, FileType.Image);

        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void CreatedAt_And_UpdatedAt_Should_Be_Equal_On_Creation()
    {
        var result = FileEntry.Create("file", "png", 100, FileSizeUnit.MB, FileType.Image);

        var entity = result.Value;

        entity.CreatedAt.Should().Be(entity.UpdatedAt);
    }
}
