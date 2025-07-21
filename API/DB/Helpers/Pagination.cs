using System.Globalization;

namespace API.DB.Helpers;

public class Pagination
{
    private const int _DefaultPageNumber = 1;
    private const int _DefaultPageSize = 25;
    private const string _DefaultSort = "asc";
    
    public int PageNumber { get; set; } = _DefaultPageNumber;
    public int PageSize { get; set; } = _DefaultPageSize;
    public string Sort { get; set; } = _DefaultSort;

    public int Offset => (PageNumber - 1) * PageSize;
    public int Limit => PageSize;

    public Pagination() { }

    public Pagination(int pageNumber, int pageSize, string sort = _DefaultSort)
    {
        PageNumber = pageNumber < 1 ? _DefaultPageNumber : pageNumber;
        PageSize = pageSize < 1 ? _DefaultPageSize : pageSize;
        Sort = sort.ToLower(CultureInfo.InvariantCulture) is "desc" ? "desc" : "asc"; // Default to asc if string is messed up
    }
}