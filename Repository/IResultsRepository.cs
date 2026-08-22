using TestTask.DTOs;
using TestTask.Entities;

namespace TestTask.Repository;

public interface IResultsRepository
{
    Task<List<Result>> GetResultsAsync(ResultFilterDto filterDto, CancellationToken cancellationToken);
}