using StudentProgressTracker.Shared.Ids;

namespace StudentProgressTracker.Core.Application.Courses.List;

public sealed record GetStudentListQueryResult
{
    public required StudentId Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
}