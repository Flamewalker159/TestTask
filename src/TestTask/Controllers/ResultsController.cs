using Microsoft.AspNetCore.Mvc;
using TestTask.DTOs;
using TestTask.Services;

namespace TestTask.Controllers;

[ApiController]
[Route("api/results")]
public class ResultsController(IResultsService resultsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetResults([FromQuery] ResultFilterDto filterDto,
        CancellationToken cancellationToken)
    {
        var result = await resultsService.GetResultsAsync(filterDto, cancellationToken);
        return Ok(result);
    }
}