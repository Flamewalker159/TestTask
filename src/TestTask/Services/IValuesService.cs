using TestTask.Entities;

namespace TestTask.Services;

public interface IValuesService
{
    Task<List<Values>> GetLatestValuesAsync(string fileName, CancellationToken cancellationToken);
}