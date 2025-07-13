using Ardalis.Specification;
using StudentProgressTracker.Domain.Models;
using StudentProgressTracker.Domain.Repositories;
using StudentProgressTracker.Persistence.Persistence;
using StudentProgressTracker.Shared.Repositories;

namespace StudentProgressTracker.Core.Application.Students;

public class StudentsRepository : Repository<Student>, IStudentsRepository
{
    public StudentsRepository(StudentContext dbContext)
        : base(dbContext) { }

    public StudentsRepository(
        StudentContext dbContext,
        ISpecificationEvaluator specificationEvaluator
    )
        : base(dbContext, specificationEvaluator) { }
}
