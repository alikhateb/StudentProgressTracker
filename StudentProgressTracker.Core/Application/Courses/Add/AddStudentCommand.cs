using MediatR;

namespace StudentProgressTracker.Core.Application.Courses.Add;

public sealed record AddStudentCommand : IRequest
{
    public required string Name { get; init; }
    public required string Email { get; init; }
}
