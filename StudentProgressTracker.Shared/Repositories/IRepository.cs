using Ardalis.Specification;

namespace StudentProgressTracker.Shared.Repositories;

public interface IRepository<T> : IRepositoryBase<T>
    where T : class
{
    IQueryable<TResult> ToQueryable<TResult>(ISpecification<T, TResult> specification);
}
