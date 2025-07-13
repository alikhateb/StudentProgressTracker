namespace StudentProgressTracker.Shared.Paging;

public class RequestOptions
{
    public PagingOption PagingOption { get; set; } = new();

    public List<FilterOptions> FilterOptions { get; set; } = [];

    public SortingField? SortingField { get; set; }
}
