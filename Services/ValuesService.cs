using TestTask.Entities;
using TestTask.Repository;

namespace TestTask.Services;

public class ValuesService(IValuesRepository valuesRepository) : IValuesService
{
    public Task<List<Values>> GetLatestValuesAsync(string fileName, CancellationToken cancellationToken)
    {
        var values = valuesRepository.GetLatestValuesAsync(fileName, cancellationToken);
        return values;
    }
}