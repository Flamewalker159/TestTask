using Microsoft.AspNetCore.Mvc;
using TestTask.Services;

namespace TestTask.Controllers;

[ApiController]
[Route("api/file-imports")]
public class FileImportController(IFileImportService fileImportService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Import(IFormFile? file, CancellationToken cancellationToken)
    {
        if (file == null) return BadRequest();
        await fileImportService.ImportAsync(file, cancellationToken);
        return Ok();
    }
}