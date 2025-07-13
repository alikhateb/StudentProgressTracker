using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StudentProgressTracker.Shared.Repositories;

public class Repository<T> : RepositoryBase<T>, IRepository<T>
    where T : class
{
    public Repository(DbContext dbContext)
        : base(dbContext) { }

    public Repository(DbContext dbContext, ISpecificationEvaluator specificationEvaluator)
        : base(dbContext, specificationEvaluator) { }

    public virtual IQueryable<TResult> ToQueryable<TResult>(
        ISpecification<T, TResult> specification
    )
    {
        return ApplySpecification(specification);
    }
}
