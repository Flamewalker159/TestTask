using System.Globalization;
using CsvHelper.Configuration;
using TestTask.DTOs;

namespace TestTask.Csv;

public class CsvFileReader : ICsvFileReader
{
    public async Task<List<CsvValueDto>> ReadCsvAsync(IFormFile file, CancellationToken cancellationToken)
    {
        await using var stream = file.OpenReadStream();

        using var reader = new StreamReader(stream);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";"
        };
        using var csv = new CsvHelper.CsvReader(reader, config);

        var records = new List<CsvValueDto>();

        await foreach (var record in csv.GetRecordsAsync<CsvValueDto>(cancellationToken))
        {
            records.Add(record);
        }

        return records;
    }
}