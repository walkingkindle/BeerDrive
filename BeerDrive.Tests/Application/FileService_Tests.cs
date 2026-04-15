using BeerDrive.Application;
using BeerDrive.Application.Abstractions.Persistence;
using BeerDrive.Application.Abstractions.Storage;
using BeerDrive.Application.Models;
using BeerDrive.Domain.Entities;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Moq;

namespace BeerDrive.Tests.Application;

public class FileService_Tests
{
    private readonly Mock<IFileStorage> _mockFileStorage;

    private readonly Mock<IFileRepository> _mockFileRepository;
    private readonly Mock<ILogger<FileEntryService>> _mockLogger;
    private readonly FileEntryService _service;
    public FileService_Tests()
    {
        _mockFileStorage = new Mock<IFileStorage>();

        _mockFileRepository = new Mock<IFileRepository>();

        _mockLogger = new Mock<ILogger<FileEntryService>>();

        _service = new FileEntryService(_mockFileStorage.Object, _mockFileRepository.Object, _mockLogger.Object); 
        
    }
    [Fact]
    public async Task SaveAsync_Fails_Validation_Domain()
    {
        var result = await _service.UploadFile(new UploadFileRequest { FileName = "something", Extension = "pnb", FileStream = new FormFile(Stream.Null, 0, 0, "aezakmi", "aezakmi1123"), Type = FileType.Ppt, Unit = FileSizeUnit.MB });

        result.IsSuccess.Should().BeFalse();

        result.Error.Should().NotBeNull();
    }
}
