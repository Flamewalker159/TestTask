using System.ComponentModel.DataAnnotations;

namespace TestTask.Entities;

public class FileImport
{
    [Key] public int Id { get; set; }

    [Required] public string FileName { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }
    public List<Values> Values { get; set; } = [];
    public Result? Result { get; set; }
}