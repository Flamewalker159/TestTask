using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Entities;

namespace TestTask.Repository;

public class ValuesRepository(AppDbContext dbContext) : IValuesRepository
{
    public async Task<List<Values>> GetLatestValuesAsync(string fileName, CancellationToken cancellationToken)
    {
        return await dbContext.Values
            .AsNoTracking()
            .Where(x => x.FileImport.FileName == fileName)
            .OrderByDescending(x => x.Date)
            .Take(10)
            .ToListAsync(cancellationToken);
    }
}