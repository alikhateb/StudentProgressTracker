using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentProgressTracker.Domain.Models;

namespace StudentProgressTracker.Persistence.EntityConfiguration;

internal class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name).HasMaxLength(100);

        builder.Property(x => x.Email).HasMaxLength(100);

        builder.HasMany(x => x.Progresses).WithOne(x => x.Student).HasForeignKey(x => x.StudentId);
    }
}
