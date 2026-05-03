using BeerDrive.Application;
using BeerDrive.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeerDrive.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UploadController(FileEntryService fileEntryService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadFile(UploadFileRequest request)
    {
        var result = await fileEntryService.UploadFile(request);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }
}
