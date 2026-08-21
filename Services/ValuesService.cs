using System.Globalization;
using TestTask.Data;
using TestTask.Exceptions;
using CsvHelper;
using TestTask.Entities;

namespace TestTask.Services;

public class ValuesService(AppDbContext dbContext, CsvValidate csvValidate) : IValuesService
{
    public async Task InputFromCsvAsync(IFormFile file,CancellationToken cancellationToken)
    {
        if (file is null)
        {
            throw new BadRequestExceptions("Файл пустой");
        }

        var records = await ReadCsvAsync(file, cancellationToken);
     
        if (records.Count == 0)
        {
            throw new BadRequestExceptions(
                "CSV файл не содержит записей.");
        }

        if (records.Count > 10_000)
        {
            throw new BadRequestExceptions(
                "CSV файл не может содержать больше 10000 записей.");
        }

        // валидация
        var currentTime = DateTimeOffset.UtcNow;
        var values = new List<Values>(records.Count);
        for (var i = 0; i < records.Count; i++)
        {
            var record = records[i];
            csvValidate.Validate(record, i + 2, currentTime);
            values.Add(new Values
            {
                Date = record.Date,
                ExecutionTime = record.ExecutionTime,
                Value = record.Value
            });
        }

        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            dbContext.Values.AddRange(values);

            await dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private async Task<List<Values>> ReadCsvAsync(IFormFile file, CancellationToken cancellationToken)
    {
        await using var stream = file.OpenReadStream();

        using var reader = new StreamReader(stream);

        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var records = new List<Values>();

        await foreach (var record in csv.GetRecordsAsync<Values>(cancellationToken))
        {
            records.Add(record);
        }

        return records;
    }
}