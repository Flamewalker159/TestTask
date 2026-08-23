namespace TestTask.Services;

public interface IFileImportService
{
    Task ImportAsync(IFormFile file, CancellationToken cancellationToken);
}