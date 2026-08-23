using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask.Entities;

public class Result
{
    [Key] public int Id { get; set; }

    [ForeignKey("FileImportId")] public int FileImportId { get; set; }

    public FileImport FileImport { get; set; } = null!;
    public double TimeDelta { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public double AverageExecutionTime { get; set; }
    public double AverageValue { get; set; }
    public double MedianValue { get; set; }
    public double MaxValue { get; set; }
    public double MinValue { get; set; }
}