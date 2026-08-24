using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Entities;
using TestTask.Exceptions;

namespace TestTask.Repository;

public class ValuesRepository(AppDbContext dbContext) : IValuesRepository
{
    public async Task<List<Values>> GetLatestValuesAsync(string fileName, CancellationToken cancellationToken)
    {
        var result = await dbContext.Values
            .AsNoTracking()
            .Where(x => x.FileImport.FileName == fileName)
            .OrderByDescending(x => x.Date)
            .Take(10)
            .ToListAsync(cancellationToken);
        return result.Count == 0 ? throw new NotFoundException("Не найдено") : result;
    }
}