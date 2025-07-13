namespace StudentProgressTracker.Shared.Paging;

public sealed class FilterOptions
{
    public string PropertyName { get; set; }

    public string Value { get; set; }

    public FilterOperation Operation { get; set; }
}

public enum FilterOperation
{
    Contains,
    NotContains,
    Equal,
    NotEqual,
    GreaterThan,
    LessThan,
    GreaterThanOrEqual,
    LessThanOrEqual,
}
