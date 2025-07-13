using StudentProgressTracker.Shared.Ids;

namespace StudentProgressTracker.Domain.Models;

public class Progress
{
    public Progress(
        StudentId studentId,
        CourseId courseId,
        DateTime enrollmentDate,
        decimal completedHours,
        decimal score
    )
    {
        StudentId = studentId;
        CourseId = courseId;
        EnrollmentDate = enrollmentDate;
        CompletedHours = completedHours;
        Score = score;
    }

    private Progress() { }

    public StudentId StudentId { get; set; }
    public Student Student { get; set; }
    public CourseId CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public decimal CompletedHours { get; set; }
    public decimal Score { get; set; }
    public bool IsComplete => Course.CreditHours == CompletedHours;
}
