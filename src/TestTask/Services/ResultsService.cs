using TestTask.DTOs;
using TestTask.Repository;

namespace TestTask.Services;

public class ResultsService(IResultsRepository resultsRepository) : IResultsService
{
    public async Task<List<ResultDto>> GetResultsAsync(ResultFilterDto filterDto, CancellationToken cancellationToken)
    {
        var results = await resultsRepository.GetResultsAsync(filterDto, cancellationToken);
        return results.Select(result => new ResultDto
        {
            FileName = result.FileImport.FileName,
            TimeDelta = result.TimeDelta,
            StartDate = result.StartDate,
            AverageExecutionTime = result.AverageExecutionTime,
            AverageValue = result.AverageValue,
            MedianValue = result.MedianValue,
            MaxValue = result.MaxValue,
            MinValue = result.MinValue
        }).ToList();
    }
}