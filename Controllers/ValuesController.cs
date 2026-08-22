using Microsoft.AspNetCore.Mvc;
using TestTask.Services;

namespace TestTask.Controllers;

[ApiController]
[Route("/api/values")]
public class ValuesController(IValuesService valuesService) : ControllerBase
{
    [HttpGet("{fileName}")]
    public async Task<IActionResult> GetLatestValuesAsync(string fileName, CancellationToken cancellationToken)
    {
        var values = await valuesService.GetLatestValuesAsync(fileName, cancellationToken);
        return Ok(values);
    }
}