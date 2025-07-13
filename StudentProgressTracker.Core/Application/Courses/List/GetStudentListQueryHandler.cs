using MediatR;
using StudentProgressTracker.Domain.Repositories;
using StudentProgressTracker.Shared.Extensions;
using StudentProgressTracker.Shared.Paging;

namespace StudentProgressTracker.Core.Application.Courses.List;

internal sealed class GetStudentListQueryHandler(IStudentsRepository studentsRepository)
    : IRequestHandler<GetStudentListQuery, PageResponse<GetStudentListQueryResult>>
{
    public async Task<PageResponse<GetStudentListQueryResult>> Handle(
        GetStudentListQuery request,
        CancellationToken cancellationToken
    )
    {
        var specification = new GetStudentListQuerySpecification();
        var result = await studentsRepository
            .ToQueryable(specification)
            .WithPagingOptions(request, cancellationToken: cancellationToken);

        return result;
    }
}