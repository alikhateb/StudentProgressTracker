using StudentProgressTracker.Domain.Common;
using StudentProgressTracker.Shared.Ids;

namespace StudentProgressTracker.Domain.Models;

public class Student : Auditable, IBaseEntity<StudentId>
{
    private readonly List<Progress> _progresses = [];

    public Student(string name, string email)
    {
        Id = StudentId.New();
        Name = name;
        Email = email;
    }

    private Student() { }

    public StudentId Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public IReadOnlyList<Progress> Progresses => _progresses.AsReadOnly();
}
