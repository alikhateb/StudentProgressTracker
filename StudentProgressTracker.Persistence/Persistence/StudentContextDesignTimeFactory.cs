using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StudentProgressTracker.Persistence.Persistence;

public class StudentContextDesignTimeFactory : IDesignTimeDbContextFactory<StudentContext>
{
    public StudentContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StudentContext>();
        optionsBuilder.UseStronglyTypeConverters();
        optionsBuilder.UseSqlServer(
            @"Data Source=(localdb)\\ProjectModels;Initial Catalog=student_progress_tracker;Integrated Security=True"
        );

        return new StudentContext(optionsBuilder.Options);
    }
}
