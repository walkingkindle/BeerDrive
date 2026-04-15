using BeerDrive.Domain.Entities;
using BeerDrive.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BeerDrive.Tests.Infrastructure;

public class FileEntry_Tests
{
    [Fact]
    public void File_Entry_Can_Create()
    {
        var options = new DbContextOptionsBuilder<BeerDriveDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        using var context = new BeerDriveDbContext(options);

        var entry = FileEntry.Create("file", "png", 100, FileSizeUnit.MB, FileType.Image).Value;

        context.FileEntries.Add(entry);

        context.SaveChanges();

        var savedEntry = context.FileEntries.First().Name;

        savedEntry.Name.Should().Be("file");
    }
    
}
