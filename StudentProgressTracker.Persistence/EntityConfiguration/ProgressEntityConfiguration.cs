using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentProgressTracker.Domain.Models;

namespace StudentProgressTracker.Persistence.EntityConfiguration;

internal class ProgressEntityConfiguration : IEntityTypeConfiguration<Progress>
{
    public void Configure(EntityTypeBuilder<Progress> builder)
    {
        builder.ToTable("Student_Course_Progress");

        builder.HasKey(x => new { x.StudentId, x.CourseId });

        builder.Property(x => x.CompletedHours);

        builder.Property(x => x.Score);

        builder.Ignore(x => x.IsComplete);
    }
}
