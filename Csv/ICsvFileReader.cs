using TestTask.DTOs;
using TestTask.Entities;

namespace TestTask.Csv;

public interface ICsvFileReader
{
    Task<List<CsvValueDto>> ReadCsvAsync(IFormFile file, CancellationToken cancellationToken);
}