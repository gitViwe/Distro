namespace Shared;

/// <summary>
/// A unified return type for the API endpoint
/// </summary>
/// <typeparam name="TData">The data type returned from the request</typeparam>
public class PaginatedResult<TData> : Result where TData : class, new()
{
    /// <summary>
    /// Instantiate a new page-able result to return 
    /// </summary>
    /// <param name="succeeded">Flags whether the process was successful</param>
    /// <param name="data">The content returned from the request</param>
    /// <param name="messages">The response messages</param>
    /// <param name="count">The total number of items</param>
    /// <param name="page">The current page number</param>
    /// <param name="pageSize">The number of items in a single page</param>
    internal PaginatedResult(bool succeeded, IEnumerable<TData> data, IEnumerable<string> messages, int count = 0, int page = 1, int pageSize = 15)
    {
        Succeeded = succeeded;
        Data = data;
        Messages = messages;
        TotalCount = count;
        CurrentPage = page;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

    }


    public static PaginatedResult<TData> Failure()
    {
        return new PaginatedResult<TData>(false, new List<TData>(), new List<string>());
    }

    public static PaginatedResult<TData> Failure(string message)
    {
        return new PaginatedResult<TData>(false, new List<TData>(), new List<string> { message });
    }

    public static PaginatedResult<TData> Failure(IEnumerable<string> messages)
    {
        return new PaginatedResult<TData>(false, new List<TData>(), messages);
    }

    public static PaginatedResult<TData> Success(IEnumerable<TData> data, int count, int page, int pageSize)
    {
        return new PaginatedResult<TData>(true, data, new List<string>(), count, page, pageSize);
    }

    /// <summary>
    /// The content returned from the request
    /// </summary>
    public IEnumerable<TData> Data { get; set; } = new List<TData>();

    /// <summary>
    /// The current page number
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// The total number of pages
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// The total number of items
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// The number of items in a single page
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Determines if there are  additional pages behind
    /// </summary>
    public bool HasPreviousPage => CurrentPage > 1;

    /// <summary>
    /// Determines if there are additional pages ahead
    /// </summary>
    public bool HasNextPage => CurrentPage < TotalPages;
}
