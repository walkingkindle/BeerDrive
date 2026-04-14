using BeerDrive.Domain.Entities;
using FluentAssertions;

namespace BeerDrive.Tests.Domain
{
    public class FileSize_Tests
    {
        [Fact]
        public void Create_Should_Succeed_With_Valid_Data()
        {
            var result = FileSize.Create(100, "MB");

            result.IsSuccess.Should().BeTrue();
            result.Value.SizeValue.Should().Be(100);
            result.Value.Unit.Should().Be("MB");
        }

        [Fact]
        public void Create_Should_Fail_When_Size_Is_Negative()
        {
            var result = FileSize.Create(-1, "MB");

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("Value must be a valid numebr");
        }

        [Fact]
        public void Create_Should_Fail_When_Unit_Is_Empty()
        {
            var result = FileSize.Create(100, "");

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("Unit must be a valid value");
        }

        [Fact]
        public void Create_Should_Fail_When_Unit_Is_Unsupported()
        {
            var result = FileSize.Create(100, "TBX");

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("Unsupported file unit");
        }

        [Fact]
        public void Two_FileSizes_With_Same_Values_Should_Be_Equal()
        {
            var first = FileSize.Create(100, "MB").Value;
            var second = FileSize.Create(100, "MB").Value;

            first.Should().Be(second);
        }

        [Fact]
        public void Two_FileSizes_With_Different_Values_Should_Not_Be_Equal()
        {
            var first = FileSize.Create(100, "MB").Value;
            var second = FileSize.Create(200, "MB").Value;

            first.Should().NotBe(second);
        }

        [Fact]
        public void Two_FileSizes_With_Different_Units_Should_Not_Be_Equal()
        {
            var first = FileSize.Create(100, "MB").Value;
            var second = FileSize.Create(100, "KB").Value;

            first.Should().NotBe(second);
        }

        [Fact]
        public void Equal_Objects_Should_Have_Same_HashCode()
        {
            var first = FileSize.Create(100, "MB").Value;
            var second = FileSize.Create(100, "MB").Value;

            first.GetHashCode().Should().Be(second.GetHashCode());
        }
    }
}
