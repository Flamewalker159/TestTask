using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Services;

namespace TestTask.Controllers;

[ApiController]
[Route("api/csv")]
public class ValueController(IValuesService valuesService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Import([FromForm] IFormFile? file, CancellationToken cancellationToken)
    {
        await valuesService.InputFromCsvAsync(file, cancellationToken);
        return Ok();
    }
}