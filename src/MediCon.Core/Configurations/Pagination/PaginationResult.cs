namespace MediCon.Core.Configurations.Pagination;

public class PaginationResult<T>
{
    public long TotalItems { get; set; }
    public long TotalPages { get; set; }

    public int CurrentPageNo { get; set; }
    public int CurrentPageSize { get; }

    public bool HasNextPage => CurrentPageNo < TotalPages;
    public bool HasPreviousPage => CurrentPageNo > 1;

    public IList<T> Data { get; set; } = [];

    public PaginationResult()
    {
    }

    public PaginationResult(IList<T>? data, int pageNo, int pageSize, long totalItems)
    {
        Data = data ?? [];

        CurrentPageNo = pageNo;
        CurrentPageSize = pageSize;

        var totalPage = totalItems / pageSize;
        if (totalItems % pageSize != 0)
        {
            totalPage++;
        }

        TotalItems = totalItems;
        TotalPages = totalPage;
    }
}
