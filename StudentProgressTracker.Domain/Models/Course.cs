using StudentProgressTracker.Domain.Common;
using StudentProgressTracker.Shared.Ids;

namespace StudentProgressTracker.Domain.Models;

public class Course : Auditable, IBaseEntity<CourseId>
{
    private readonly List<Progress> _progresses = [];

    public Course(string code, string title, decimal creditHours)
    {
        Id = CourseId.New();
        Code = code;
        Title = title;
        CreditHours = creditHours;
    }

    private Course() { }

    public CourseId Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public decimal CreditHours { get; set; }
    public IReadOnlyList<Progress> Progresses => _progresses.AsReadOnly();
}
