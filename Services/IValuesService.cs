namespace TestTask.Services;

public interface IValuesService
{
    Task ImportAsync(IFormFile file, CancellationToken cancellationToken);
}