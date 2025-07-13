using MediatR;
using StudentProgressTracker.Shared.Paging;

namespace StudentProgressTracker.Core.Application.Courses.List;

public sealed class GetStudentListQuery
    : RequestOptions,
        IRequest<PageResponse<GetStudentListQueryResult>>;
