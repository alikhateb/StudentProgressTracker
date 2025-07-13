using Ardalis.Specification;
using StudentProgressTracker.Domain.Models;
using StudentProgressTracker.Shared.Ids;

namespace StudentProgressTracker.Core.Application.Courses.Details;

internal sealed class GetStudentDetailsQuerySpecification
    : Specification<Student, GetStudentDetailsQueryResult>
{
    public GetStudentDetailsQuerySpecification(StudentId id)
    {
        Query.Where(x => x.Id == id);
        Query.Select(x => new GetStudentDetailsQueryResult
        {
            Id = x.Id,
            Name = x.Name,
            Email = x.Email,
        });
    }
}
