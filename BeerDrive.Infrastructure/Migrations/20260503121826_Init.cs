using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeerDrive.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "FileEntries",
            columns: table => new
            {
                Id = table
                    .Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                Extension = table.Column<string>(
                    type: "nvarchar(10)",
                    maxLength: 10,
                    nullable: false
                ),
                Type = table.Column<int>(type: "int", nullable: false),
                SizeValue = table.Column<long>(type: "bigint", nullable: false),
                Unit = table.Column<int>(type: "int", nullable: false),
                CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
            },
            constraints: table => table.PrimaryKey("PK_FileEntries", x => x.Id)
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "FileEntries");
    }
}
