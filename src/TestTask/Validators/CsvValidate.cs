using TestTask.DTOs;
using TestTask.Exceptions;

namespace TestTask.Validators;

public class CsvValidate
{
    private readonly DateTimeOffset _minDate =
        new(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);

    public static void ValidateCount(int count)
    {
        switch (count)
        {
            case 0:
                throw new BadRequestException(
                    "CSV файл не содержит записей.");
            case > 10_000:
                throw new BadRequestException(
                    "CSV файл не может содержать больше 10000 записей.");
        }
    }

    public void Validate(CsvValueDto record, int lineNumber, DateTimeOffset currentDate)
    {
        if (record.Date is null)
            throw new BadRequestException(
                $"Строка {lineNumber}: Date отсутствует");


        if (record.ExecutionTime is null)
            throw new BadRequestException(
                $"Строка {lineNumber}: ExecutionTime отсутствует");


        if (record.Value is null)
            throw new BadRequestException(
                $"Строка {lineNumber}: Value отсутствует");

        //date не может раньше 01.01.2000 и позже
        if (record.Date < _minDate)
            throw new BadRequestException($"Строка {lineNumber}: Date не может быть раньше 01.01.2000");
        if (record.Date > currentDate)
            throw new BadRequestException($"Строка {lineNumber}: Date не может быть позже {currentDate}");
        //executiontime !<= 0
        if (record.ExecutionTime < 0)
            throw new BadRequestException($"Строка {lineNumber}: ExecutionTime не может быть меньше 0");

        //value !<= 0
        if (record.Value < 0) throw new BadRequestException($"Строка {lineNumber}: Value не может быть меньше 0");
    }
}