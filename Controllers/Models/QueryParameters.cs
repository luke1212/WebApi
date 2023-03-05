namespace WebApi.Models;

public class QueryParameters
{
    //max size of page
    const int _maxPageSize = 50;

    //page size
    private int _pageSize = 10;

    public int PageNumber { get; set; } = 1;
    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = (value > _maxPageSize) ? _maxPageSize : value; }
    }
    public string SortBy { get; set; } = string.Empty;
    public string SortOrder { get; set; } = "asc";
    public string SearchTerm { get; set; } = string.Empty;
}