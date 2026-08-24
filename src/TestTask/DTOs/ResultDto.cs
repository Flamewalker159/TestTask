namespace TestTask.DTOs;

public class ResultDto
{
    public string FileName { get; set; } = null!;
    public double TimeDelta { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public double AverageExecutionTime { get; set; }
    public double AverageValue { get; set; }
    public double MedianValue { get; set; }
    public double MaxValue { get; set; }
    public double MinValue { get; set; }
}