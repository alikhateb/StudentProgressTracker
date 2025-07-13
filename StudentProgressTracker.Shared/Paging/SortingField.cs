namespace StudentProgressTracker.Shared.Paging;

public class SortingField
{
    public string PropertyName { get; set; }

    public SortDirection Direction { get; set; }
}

public enum SortDirection
{
    Ascending,
    Descending,
}
