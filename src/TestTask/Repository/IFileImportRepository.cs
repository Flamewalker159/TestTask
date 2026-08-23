using TestTask.Entities;

namespace TestTask.Repository;

public interface IFileImportRepository
{
    Task SaveAsync(FileImport fileImport, CancellationToken cancellationToken);
}