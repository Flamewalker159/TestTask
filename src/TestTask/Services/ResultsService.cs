using TestTask.DTOs;
using TestTask.Entities;
using TestTask.Repository;

namespace TestTask.Services;

public class ResultsService(IResultsRepository resultsRepository) : IResultsService
{
    public Task<List<Result>> GetResultsAsync(ResultFilterDto filterDto, CancellationToken cancellationToken)
    {
        var results = resultsRepository.GetResultsAsync(filterDto, cancellationToken);
        return results;
    }
}