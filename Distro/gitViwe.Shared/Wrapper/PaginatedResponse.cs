namespace gitViwe.Shared;

/// <summary>
/// A unified return type for the API endpoint
/// </summary>
/// <typeparam name="TData">The data type returned from the request</typeparam>
public sealed class PaginatedResponse<TData> where TData : class, new()
{
    /// <summary>
    /// Instantiate a new page-able result to return 
    /// </summary>
    public PaginatedResponse() { }

    /// <summary>
    /// Instantiate a new page-able result to return 
    /// </summary>
    /// <param name="data">The content returned from the request</param>
    /// <param name="totalCount">The total number of items</param>
    /// <param name="currentPage">The current page number</param>
    /// <param name="pageSize">The number of items in a single page</param>
    private PaginatedResponse(IEnumerable<TData> data, int totalCount, int currentPage, int pageSize)
    {
        Data = data;
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }

    /// <summary>
    /// A failed response
    /// </summary>
    /// <returns>An empty instance of <typeparamref name="TData"/></returns>
    public static PaginatedResponse<TData> Fail()
    {
        return new PaginatedResponse<TData>();
    }

    /// <summary>
    /// A successful response
    /// </summary>
    /// <param name="data">The content returned from the request</param>
    /// <param name="totalCount">The total number of items</param>
    /// <param name="currentPage">The current page number</param>
    /// <param name="pageSize">The number of items in a single page</param>
    /// <returns>An instance of <typeparamref name="TData"/></returns>
    public static PaginatedResponse<TData> Success(IEnumerable<TData> data, int totalCount, int currentPage, int pageSize)
    {
        if (IsValidPagination(data, currentPage, pageSize, out _))
        {
            return new PaginatedResponse<TData>(data, totalCount, currentPage, pageSize); 
        }

        return new PaginatedResponse<TData>(data?.Count() > 0 ? data : [], totalCount, 1, 15);
    }

    /// <summary>
    /// Paginates the <paramref name="dataToPaginate"/> and returns a successful response
    /// </summary>
    /// <param name="dataToPaginate">The content to paginate and return</param>
    /// <param name="currentPage">The current page number</param>
    /// <param name="pageSize">The number of items in a single page</param>
    /// <returns>An instance of <typeparamref name="TData"/> if pagination is valid. Else an empty instance of <typeparamref name="TData"/></returns>
    public static PaginatedResponse<TData> Success(IEnumerable<TData> dataToPaginate, int currentPage, int pageSize)
    {
        if (IsValidPagination(dataToPaginate, currentPage, pageSize, out int count))
        {
            return new PaginatedResponse<TData>(dataToPaginate.Skip((currentPage - 1) * pageSize).Take(pageSize), count, currentPage, pageSize);
        }

        return new PaginatedResponse<TData>(count > 0 ? dataToPaginate.Take(15) : [], count, 1, 15);
    }

    private static bool IsValidPagination(IEnumerable<TData> dataToPaginate, int page, int pageSize, out int count)
    {
        if (dataToPaginate is null)
        {
            count = 0;
            return false;
        }

        count = dataToPaginate.Count();

        int totalPages = (int)Math.Ceiling(count / (double)pageSize);

        return page <= totalPages;
    }

    /// <summary>
    /// The content returned from the request
    /// </summary>
    public IEnumerable<TData> Data { get; set; } = [];

    /// <summary>
    /// The current page number
    /// </summary>
    public int CurrentPage { get; set; } = 1;

    /// <summary>
    /// The total number of items
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// The number of items in a single page
    /// </summary>
    public int PageSize { get; set; } = 15;

    /// <summary>
    /// The total number of pages
    /// </summary>
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    /// <summary>
    /// Determines if there are  additional pages behind
    /// </summary>
    public bool HasPreviousPage => CurrentPage > 1;

    /// <summary>
    /// Determines if there are additional pages ahead
    /// </summary>
    public bool HasNextPage => CurrentPage < TotalPages;
}
