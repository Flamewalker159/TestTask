using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Entities;

namespace TestTask.Repository;

public class FileImportRepository(AppDbContext dbContext) : IFileImportRepository
{
    public async Task SaveAsync(FileImport fileImport, CancellationToken cancellationToken)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        var existingFile = await dbContext.FileImports.FirstOrDefaultAsync(x => x.FileName == fileImport.FileName, cancellationToken);

        if (existingFile is not null)
        {
            dbContext.FileImports.Remove(existingFile);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        try
        {
            dbContext.FileImports.Add(fileImport);

            await dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}