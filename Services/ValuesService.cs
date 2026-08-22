using TestTask.Calculators;
using TestTask.Csv;
using TestTask.Entities;
using TestTask.Mappings;
using TestTask.Repository;
using TestTask.Validators;

namespace TestTask.Services;

public class ValuesService(
    ICsvFileReader csvFileReader,
    CsvValidate validator,
    IResultCalculator calculator,
    IFileImportRepository importRepository) : IValuesService
{
    public async Task ImportAsync(IFormFile file, CancellationToken cancellationToken)
    {
        //чтение csv
        var records = await csvFileReader.ReadCsvAsync(file, cancellationToken);

        // валидация
        CsvValidate.ValidateCount(records.Count);

        var values = new List<Values>(records.Count);

        for (var i = 0; i < records.Count; i++)
        {
            var record = records[i];
            validator.Validate(record, i + 2, DateTimeOffset.UtcNow);
            values.Add(CsvValueMapper.Map(record));
        }

        //подсчет результатов
        var result = calculator.Calculate(values);

        //сохранение
        var fileImport = new FileImport
        {
            FileName = file.FileName,
            CreatedAt = DateTimeOffset.UtcNow,
            Values = values,
            Result = result
        };
        await importRepository.SaveAsync(fileImport, cancellationToken);
    }
}