using System.ComponentModel.DataAnnotations;

namespace TestTask.Entities;

public class Values
{
    public int Id { get; set; }
    public DateTimeOffset Date { get; set; }
    public double ExecutionTime { get; set; }
    public double Value { get; set; }
}