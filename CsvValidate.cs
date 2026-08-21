using System.Globalization;
using TestTask.DTOs;
using TestTask.Entities;
using TestTask.Exceptions;

namespace TestTask;

public class CsvValidate
{
    private readonly DateTimeOffset MinDate = new(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);

    public void Validate(Values values, int lineNumber, DateTimeOffset currentDate)
    {
        //date не может раньше 01.01.2000 и позже
        if (values.Date < MinDate)
        {
            throw new BadRequestExceptions($"Строка {lineNumber}: Date не может быть раньше 01.01.2000");
        }
        if (values.Date > currentDate)
        {
            throw new BadRequestExceptions($"Строка {lineNumber}: Date не может быть позже {currentDate}");
        }
        //executiontime !<= 0
        if (values.ExecutionTime < 0)
        {
            throw new BadRequestExceptions($"Строка {lineNumber}: ExecutionTime не может быть меньше 0");
        }

        //value !<= 0
        if (values.Value < 0)
        {
            throw new BadRequestExceptions($"Строка {lineNumber}: Value не может быть меньше 0");
        }
    }
}