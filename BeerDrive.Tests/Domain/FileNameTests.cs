using BeerDrive.Domain.Entities;
using CSharpFunctionalExtensions;

namespace BeerDrive.Tests.Domain;

public class FileNameTests
{
    [Fact]
    public void FileName_Create_Should_Succeed()
    {
        string extension = "txt";

        string fileName = "aezakmi";

        Result<FileName> result = FileName.Create(fileName, extension);

        Assert.True(result.IsSuccess);

        Assert.Equal(result.Value.Name, fileName);

        Assert.Equal(result.Value.Extension, extension);

    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void FileName_Create_Should_Fail_NullFileName(string? filename)
    {
        string extension = ".txt";

        Result<FileName> result = FileName.Create(extension, filename!);

        Assert.False(result.IsSuccess);
    }

    [Theory]
    [InlineData("jpg")]
    [InlineData("pdf")]
    [InlineData("xlsx")]
    public void FileName_Create_Should_Fail_UnsupportedExtensions(string extension)
    {
        Result<FileName> result = FileName.Create("aezakmi", extension);

        Assert.False(result.IsSuccess);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
#pragma warning disable xUnit1012 // Null should only be used for nullable parameters
    [InlineData(null)]
#pragma warning restore xUnit1012 // Null should only be used for nullable parameters
    public void FileName_Create_Should_Fail_Null_Extension(string extension)
    {
        var result = FileName.Create("aezakmi", extension);

        Assert.False(result.IsSuccess);
    }

    [Theory]
    [InlineData("aezakmi", "txt")]
    [InlineData("hesoyam", "ppt")]
    [InlineData("uzuwyww","png")]
    public void Equals_Is_True(string name, string extension)
    {
        var result = FileName.Create(name, extension);

        var result2 = FileName.Create(name, extension);

        Assert.True(result.Equals(result2));
    }

    [Theory]
    [InlineData("aezakmi", "txt")]
    public void Equals_Is_False_Nulls(string name, string extension)
    {
        var result = FileName.Create(name, extension);

        Assert.False(result.Equals(null));
    }

    [Fact]
    public void Equals_Is_False()
    {
        var result = FileName.Create("aezakmi", "txt");

        var resultDiff = FileName.Create("hesoyam", "ppt");
        Assert.False(result.Equals(resultDiff));
    }



}
