using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StudentProgressTracker.Shared.Paging;

namespace StudentProgressTracker.Shared.Extensions;

public static class RequestOptionsExtensions
{
    public static async Task<PageResponse<T>> WithPagingOptions<T>(
        this IQueryable<T> query,
        RequestOptions requestOptions,
        CancellationToken cancellationToken = default
    )
    {
        var filterOptions = requestOptions.FilterOptions;
        var pagingOption = requestOptions.PagingOption;
        var sortingField = requestOptions.SortingField;

        if (sortingField is not null)
        {
            query = query.ApplySorting(sortingField);
        }

        if (filterOptions.Count != 0)
        {
            query = query.ApplyFilter(filterOptions);
        }

        var count = await query.CountAsync(cancellationToken);
        var result = await query
            .Skip((pagingOption.PageNumber - 1) * pagingOption.PageSize)
            .Take(pagingOption.PageSize)
            .ToListAsync(cancellationToken);

        return new PageResponse<T>(result, count, pagingOption.PageNumber, pagingOption.PageSize);
    }

    public static IQueryable<T> ApplyFilter<T>(
        this IQueryable<T> query,
        List<FilterOptions> filterOptions
    )
    {
        if (filterOptions.Any(x => string.IsNullOrWhiteSpace(x.PropertyName) || x.Value is null))
        {
            throw new AggregateException(message: "one or more filters are not valid.");
        }

        var parameter = Expression.Parameter(typeof(T), "x");
        Expression combined = null!;

        foreach (var filterOption in filterOptions)
        {
            var property = Expression.Property(parameter, filterOption.PropertyName);

            // Handle nullable types
            if (Nullable.GetUnderlyingType(property.Type) != null)
            {
                var hasValue = Expression.Property(property, "HasValue");
                var valueProperty = Expression.Property(property, "Value");

                var convertedValue = Convert.ChangeType(
                    filterOption.Value,
                    Nullable.GetUnderlyingType(property.Type)!
                );
                var constant = Expression.Constant(convertedValue);

                Expression operation = filterOption.Operation switch
                {
                    FilterOperation.Equal => Expression.Equal(valueProperty, constant),
                    FilterOperation.GreaterThan => Expression.GreaterThan(valueProperty, constant),
                    FilterOperation.LessThan => Expression.LessThan(valueProperty, constant),
                    FilterOperation.GreaterThanOrEqual => Expression.GreaterThanOrEqual(
                        valueProperty,
                        constant
                    ),
                    FilterOperation.LessThanOrEqual => Expression.LessThanOrEqual(
                        valueProperty,
                        constant
                    ),
                    FilterOperation.NotEqual => Expression.NotEqual(valueProperty, constant),
                    FilterOperation.Contains => Expression.Call(
                        Expression.Call(valueProperty, "ToLower", null),
                        typeof(string).GetMethod("Contains", [typeof(string)])!,
                        Expression.Call(constant, "ToLower", null)
                    ),
                    FilterOperation.NotContains => Expression.Not(
                        Expression.Call(
                            Expression.Call(valueProperty, "ToLower", null),
                            typeof(string).GetMethod("Contains", [typeof(string)])!,
                            Expression.Call(constant, "ToLower", null)
                        )
                    ),
                    _ => throw new ArgumentException(
                        $"Unsupported operator: {filterOption.Operation}"
                    ),
                };

                var andExpression = Expression.AndAlso(hasValue, operation);

                combined =
                    combined == null ? operation : Expression.AndAlso(combined, andExpression);
            }
            else
            {
                // Non-nullable version from earlier
                var convertedValue = Convert.ChangeType(filterOption.Value, property.Type);
                var constant = Expression.Constant(convertedValue);

                Expression operation = filterOption.Operation switch
                {
                    FilterOperation.Equal => Expression.Equal(property, constant),
                    FilterOperation.GreaterThan => Expression.GreaterThan(property, constant),
                    FilterOperation.LessThan => Expression.LessThan(property, constant),
                    FilterOperation.GreaterThanOrEqual => Expression.GreaterThanOrEqual(
                        property,
                        constant
                    ),
                    FilterOperation.LessThanOrEqual => Expression.LessThanOrEqual(
                        property,
                        constant
                    ),
                    FilterOperation.NotEqual => Expression.NotEqual(property, constant),
                    FilterOperation.Contains => Expression.Call(
                        Expression.Call(property, "ToLower", null),
                        typeof(string).GetMethod("Contains", [typeof(string)])!,
                        Expression.Call(constant, "ToLower", null)
                    ),
                    FilterOperation.NotContains => Expression.Not(
                        Expression.Call(
                            Expression.Call(property, "ToLower", null),
                            typeof(string).GetMethod("Contains", [typeof(string)])!,
                            Expression.Call(constant, "ToLower", null)
                        )
                    ),
                    _ => throw new ArgumentException(
                        $"Unsupported operator: {filterOption.Operation}"
                    ),
                };

                combined = combined == null ? operation : Expression.AndAlso(combined, operation);
            }
        }

        return query.Where(Expression.Lambda<Func<T, bool>>(combined, parameter));
    }

    public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, SortingField sortingField)
    {
        if (sortingField == null || string.IsNullOrWhiteSpace(sortingField.PropertyName))
            return query;

        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, sortingField.PropertyName);
        var lambda = Expression.Lambda(property, parameter);

        var methodName =
            sortingField.Direction == SortDirection.Ascending ? "OrderBy" : "OrderByDescending";

        var result =
            (IOrderedQueryable<T>)
                typeof(Queryable)
                    .GetMethods()
                    .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), property.Type)
                    .Invoke(null, [query, lambda])!;

        return result;
    }
}
