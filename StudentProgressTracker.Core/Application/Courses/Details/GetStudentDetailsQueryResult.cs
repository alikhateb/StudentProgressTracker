using StudentProgressTracker.Shared.Ids;

namespace StudentProgressTracker.Core.Application.Courses.Details;

public sealed record GetStudentDetailsQueryResult
{
    public required StudentId Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
}
