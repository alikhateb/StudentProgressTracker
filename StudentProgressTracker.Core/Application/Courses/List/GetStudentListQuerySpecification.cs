using Ardalis.Specification;
using StudentProgressTracker.Domain.Models;

namespace StudentProgressTracker.Core.Application.Courses.List;

internal sealed class GetStudentListQuerySpecification
    : Specification<Student, GetStudentListQueryResult>
{
    public GetStudentListQuerySpecification()
    {
        Query.Select(x => new GetStudentListQueryResult
        {
            Id = x.Id,
            Name = x.Name,
            Email = x.Email,
        });
    }
}