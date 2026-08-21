using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask.Entities;

public class Values
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("FileImportId")]
    public int FileImportId { get; set; }
    public FileImport FileImport { get; set; } = null!;
    public DateTimeOffset Date { get; set; }
    public double ExecutionTime { get; set; }
    public double Value { get; set; }
}