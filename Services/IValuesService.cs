using Microsoft.AspNetCore.Mvc;

namespace TestTask.Services;

public interface IValuesService
{
    Task InputFromCsvAsync(IFormFile file, CancellationToken cancellationToken);
}