using Microsoft.EntityFrameworkCore;
using TestTask.Entities;

namespace TestTask.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Values> Values { get; set; }
    public DbSet<FileImport> FileImports { get; set; }
    public DbSet<Result> Results { get; set; }
}