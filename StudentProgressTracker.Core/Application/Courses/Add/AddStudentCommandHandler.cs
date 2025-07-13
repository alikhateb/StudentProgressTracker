using MediatR;
using StudentProgressTracker.Domain.Models;
using StudentProgressTracker.Domain.Repositories;

namespace StudentProgressTracker.Core.Application.Courses.Add;

internal sealed record AddStudentCommandHandler(IStudentsRepository StudentsRepository)
    : IRequestHandler<AddStudentCommand>
{
    public async Task Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student(request.Name, request.Email);

        await StudentsRepository.AddAsync(student, cancellationToken);
    }
}