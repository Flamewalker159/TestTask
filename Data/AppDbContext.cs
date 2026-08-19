using Microsoft.EntityFrameworkCore;
using TestTask.Entities;

namespace TestTask.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Values> Values => Set<Values>();
}
