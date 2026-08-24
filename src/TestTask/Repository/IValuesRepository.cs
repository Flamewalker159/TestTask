using TestTask.Entities;

namespace TestTask.Repository;

public interface IValuesRepository
{
    Task<List<Values>> GetLatestValuesAsync(string fileName, CancellationToken cancellationToken);
}