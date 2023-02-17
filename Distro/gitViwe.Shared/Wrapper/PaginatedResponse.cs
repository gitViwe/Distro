namespace gitViwe.Shared;

/// <summary>
/// A unified return type for the API endpoint
/// </summary>
/// <typeparam name="TData">The data type returned from the request</typeparam>
public class PaginatedResponse<TData> where TData : class, new()
{
    /// <summary>
    /// Instantiate a new page-able result to return 
    /// </summary>
    public PaginatedResponse() { }

    /// <summary>
    /// Instantiate a new page-able result to return 
    /// </summary>
    /// <param name="data">The content returned from the request</param>
    /// <param name="count">The total number of items</param>
    /// <param name="page">The current page number</param>
    /// <param name="pageSize">The number of items in a single page</param>
    internal PaginatedResponse(IEnumerable<TData> data, int count = 0, int page = 1, int pageSize = 15)
    {
        Data = data;
        TotalCount = count;
        CurrentPage = page;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }

    /// <summary>
    /// A succcessful response
    /// </summary>
    /// <param name="data">The content returned from the request</param>
    /// <param name="count">The total number of items</param>
    /// <param name="page">The current page number</param>
    /// <param name="pageSize">The number of items in a single page</param>
    /// <returns>An instance of <typeparamref name="TData"/></returns>
    public static PaginatedResponse<TData> Success(IEnumerable<TData> data, int count, int page, int pageSize)
    {
        return new PaginatedResponse<TData>(data, count, page, pageSize);
    }

    /// <summary>
    /// The content returned from the request
    /// </summary>
    public IEnumerable<TData> Data { get; } = Enumerable.Empty<TData>();

    /// <summary>
    /// The current page number
    /// </summary>
    public int CurrentPage { get; }

    /// <summary>
    /// The total number of pages
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    /// The total number of items
    /// </summary>
    public int TotalCount { get; }

    /// <summary>
    /// The number of items in a single page
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Determines if there are  additional pages behind
    /// </summary>
    public bool HasPreviousPage => CurrentPage > 1;

    /// <summary>
    /// Determines if there are additional pages ahead
    /// </summary>
    public bool HasNextPage => CurrentPage < TotalPages;
}
