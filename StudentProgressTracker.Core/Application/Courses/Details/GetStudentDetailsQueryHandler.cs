using MediatR;
using StudentProgressTracker.Domain.Repositories;

namespace StudentProgressTracker.Core.Application.Courses.Details;

internal sealed class GetStudentDetailsQueryHandler(IStudentsRepository studentsRepository)
    : IRequestHandler<GetStudentDetailsQuery, GetStudentDetailsQueryResult>
{
    public async Task<GetStudentDetailsQueryResult> Handle(
        GetStudentDetailsQuery request,
        CancellationToken cancellationToken
    )
    {
        var specification = new GetStudentDetailsQuerySpecification(request.Id);
        var student = await studentsRepository.FirstOrDefaultAsync(
            specification,
            cancellationToken
        );

        return student;
    }
}
