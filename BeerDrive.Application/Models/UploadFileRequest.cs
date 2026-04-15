using BeerDrive.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BeerDrive.Application.Models;

public class UploadFileRequest
{
    [JsonPropertyName("stream")]
    [Required]
    public IFormFile FileStream { get; init; } = default!;

    [JsonPropertyName("fileName")]
    [StringLength(255)]
    [Required]
    public string FileName { get; init; } = default!;

    [JsonPropertyName("extension")]
    [StringLength(10)]
    [Required]
    public string Extension { get; init; } = default!;

    [JsonPropertyName("unit")]
    [StringLength(2)]
    [Required]
    public FileSizeUnit Unit { get; init; }

    [JsonPropertyName("type")]
    [Required]
    public FileType Type { get; init; }
}
