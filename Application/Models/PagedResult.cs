namespace Application.Models;

public class PagedResult<T>
{
    public IEnumerable<T> Data { get; set; } = new[] { default(T)! };

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalItems { get; set; }

    public int TotalPages { get; set; }
}
