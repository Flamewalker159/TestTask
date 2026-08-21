using TestTask.Entities;

namespace TestTask.Repository;

public interface IFileImportRepository
{
    public Task SaveAsync(FileImport fileImport, CancellationToken cancellationToken);
}