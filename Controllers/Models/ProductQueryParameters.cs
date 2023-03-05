namespace WebApi.Models;

public class ProductQueryParameters : QueryParameters
{
    private string _sortOrder = "asc";
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string sku { get; set; } = string.Empty;
    public string SortBy { get; set; } = "id";
    public string SortOrder
    {
        get { return _sortOrder; }
        set
        {
            if (_sortOrder.Equals("asc") || _sortOrder.Equals("dec"))
            {
                _sortOrder = value;
            }
        }
    }
}