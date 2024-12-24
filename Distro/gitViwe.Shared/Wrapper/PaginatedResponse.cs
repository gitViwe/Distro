namespace gitViwe.Shared;

/// <summary>
/// A unified return type for the API endpoint
/// </summary>
/// <typeparam name="TData">The data type returned from the request</typeparam>
public sealed class PaginatedResponse<TData> where TData : new()
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
    /// Paginates the <paramref name="dataToPaginate"/> and returns a successful response
    /// </summary>
    /// <param name="dataToPaginate">The sequence to return elements from</param>
    /// <param name="currentPage">The current page number</param>
    /// <param name="pageSize">The number of items in a single page</param>
    /// <returns>An instance of <typeparamref name="TData"/> if pagination is valid. Else an empty instance of <typeparamref name="TData"/></returns>
    public static PaginatedResponse<TData> Success(IEnumerable<TData> dataToPaginate, int currentPage, int pageSize)
        => ToPaginatedResponse(dataToPaginate, currentPage, pageSize);
    
    /// <summary>
    /// Paginates the <paramref name="dataToPaginate"/> and returns a successful response
    /// </summary>
    /// <param name="dataToPaginate">The sequence to return elements from</param>
    /// <param name="currentPage">The current page number</param>
    /// <param name="pageSize">The number of items in a single page</param>
    /// <returns>An instance of <typeparamref name="TData"/> if pagination is valid. Else an empty instance of <typeparamref name="TData"/></returns>
    public static PaginatedResponse<TData> Success(IQueryable<TData> dataToPaginate, int currentPage, int pageSize)
        => ToPaginatedResponse(dataToPaginate, currentPage, pageSize);

    private static PaginatedResponse<TData> ToPaginatedResponse(IEnumerable<TData> dataToPaginate, int currentPage, int pageSize)
    {
        int totalCount = dataToPaginate.Count();

        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return currentPage <= totalPages
            ? new PaginatedResponse<TData>(dataToPaginate.Skip((currentPage - 1) * pageSize).Take(pageSize), totalCount, currentPage, pageSize)
            : new PaginatedResponse<TData>(dataToPaginate.Take(15), totalCount, 1, 15);
    }
    
    private static PaginatedResponse<TData> ToPaginatedResponse(IQueryable<TData> dataToPaginate, int currentPage, int pageSize)
    {
        int totalCount = dataToPaginate.Count();

        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return currentPage <= totalPages
            ? new PaginatedResponse<TData>(dataToPaginate.Skip((currentPage - 1) * pageSize).Take(pageSize), totalCount, currentPage, pageSize)
            : new PaginatedResponse<TData>(dataToPaginate.Take(15), totalCount, 1, 15);
    }

    /// <summary>
    /// The content returned from the request
    /// </summary>
    public IEnumerable<TData> Data { get; init; } = [];

    /// <summary>
    /// The current page number
    /// </summary>
    public int CurrentPage { get; init; } = 1;

    /// <summary>
    /// The total number of items
    /// </summary>
    public int TotalCount { get; init; }

    /// <summary>
    /// The number of items in a single page
    /// </summary>
    public int PageSize { get; init; } = 15;

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
