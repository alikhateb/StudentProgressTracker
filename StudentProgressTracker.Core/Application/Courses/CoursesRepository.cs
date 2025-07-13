using Ardalis.Specification;
using StudentProgressTracker.Domain.Models;
using StudentProgressTracker.Domain.Repositories;
using StudentProgressTracker.Persistence.Persistence;
using StudentProgressTracker.Shared.Repositories;

namespace StudentProgressTracker.Core.Application.Courses;

public class CoursesRepository : Repository<Course>, ICoursesRepository
{
    public CoursesRepository(StudentContext dbContext)
        : base(dbContext) { }

    public CoursesRepository(
        StudentContext dbContext,
        ISpecificationEvaluator specificationEvaluator
    )
        : base(dbContext, specificationEvaluator) { }
}
