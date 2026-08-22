using TestTask.Entities;

namespace TestTask.Calculators;

public class ResultCalculator : IResultCalculator
{
    public Result Calculate(List<Values> records)
    {
        var maxDate = records.Max(v => v.Date);
        var minDate = records.Min(v => v.Date);

        return new Result
        {
            TimeDelta = (maxDate - minDate).TotalSeconds,
            StartDate = minDate,
            AverageExecutionTime = records.Average(v => v.ExecutionTime),
            AverageValue = records.Average(v => v.Value),
            MedianValue = CalculateMedian(records),
            MaxValue = records.Max(v => v.Value),
            MinValue = records.Min(v => v.Value)
        };
    }

    //что значит статик
    private static double CalculateMedian(List<Values> records)
    {
        var sortRecords = records.OrderBy(v => v.Value).Select(v => v.Value).ToList();
        var count = sortRecords.Count;

        if (count % 2 == 0) return (sortRecords[count / 2 - 1] + sortRecords[count / 2]) / 2;

        return sortRecords[count / 2];
    }
}