using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.DTOs;
using TestTask.Entities;
using TestTask.Exceptions;

namespace TestTask.Repository;

public class ResultsRepository(AppDbContext dbContext) : IResultsRepository
{
    public async Task<List<Result>> GetResultsAsync(ResultFilterDto filterDto, CancellationToken cancellationToken)
    {
        var query = dbContext.Results.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filterDto.FileName))
            query = query.Where(x => x.FileImport.FileName == filterDto.FileName);

        if (filterDto.StartDateFrom.HasValue) query = query.Where(x => x.StartDate >= filterDto.StartDateFrom.Value);

        if (filterDto.StartDateTo.HasValue) query = query.Where(x => x.StartDate <= filterDto.StartDateTo.Value);

        if (filterDto.AverageValueFrom.HasValue)
            query = query.Where(x => x.AverageValue >= filterDto.AverageValueFrom.Value);

        if (filterDto.AverageValueTo.HasValue)
            query = query.Where(x => x.AverageValue <= filterDto.AverageValueTo.Value);

        if (filterDto.AverageExecutionTimeFrom.HasValue)
            query = query.Where(x => x.AverageExecutionTime >= filterDto.AverageExecutionTimeFrom.Value);

        if (filterDto.AverageExecutionTimeTo.HasValue)
            query = query.Where(x => x.AverageExecutionTime <= filterDto.AverageExecutionTimeTo.Value);


        if (!query.Any())
            throw new NotFoundException("Ничего не найдено");

        return await query.ToListAsync(cancellationToken);
    }
}