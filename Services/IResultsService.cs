using TestTask.DTOs;
using TestTask.Entities;

namespace TestTask.Services;

public interface IResultsService
{
    Task<List<Result>> GetResultsAsync(ResultFilterDto filterDto, CancellationToken cancellationToken);
}