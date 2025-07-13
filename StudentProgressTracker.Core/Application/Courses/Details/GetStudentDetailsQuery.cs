using MediatR;
using StudentProgressTracker.Shared.Ids;

namespace StudentProgressTracker.Core.Application.Courses.Details;

public sealed record GetStudentDetailsQuery(StudentId Id) : IRequest<GetStudentDetailsQueryResult>;
