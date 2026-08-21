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
    public async Task<IActionResult> Import(IFormFile? file, CancellationToken cancellationToken)
    {
        if (file == null) return BadRequest();
        await valuesService.ImportAsync(file, cancellationToken);
        return Ok();
    }
}