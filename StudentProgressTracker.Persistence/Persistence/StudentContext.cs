using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace StudentProgressTracker.Persistence.Persistence;

public class StudentContext(DbContextOptions<StudentContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
