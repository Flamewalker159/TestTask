using TestTask.DTOs;
using TestTask.Entities;

namespace TestTask.Mappings;

public class CsvValueMapper
{
    public static Values Map(CsvValueDto csvValueDto)
    {
        return new Values
        {
            Date = csvValueDto.Date.Value,
            ExecutionTime = csvValueDto.ExecutionTime.Value,
            Value = csvValueDto.Value.Value
        };
    }
}