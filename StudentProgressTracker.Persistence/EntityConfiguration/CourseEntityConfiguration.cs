using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentProgressTracker.Domain.Models;

namespace StudentProgressTracker.Persistence.EntityConfiguration;

internal class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Code).HasMaxLength(20);

        builder.Property(x => x.Title).HasMaxLength(100);

        builder.Property(x => x.CreditHours);

        builder.HasMany(x => x.Progresses).WithOne(x => x.Course).HasForeignKey(x => x.CourseId);
    }
}
