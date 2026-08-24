using TestTask.DTOs;

namespace TestTask.Services;

public interface IResultsService
{
    Task<List<ResultDto>> GetResultsAsync(ResultFilterDto filterDto, CancellationToken cancellationToken);
}