namespace Shared;

/// <summary>
/// A unified return type for the API endpoint
/// </summary>
/// <typeparam name="TData">The data type returned from the request</typeparam>
public class PaginatedResponse<TData> : Response where TData : class, new()
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
    internal PaginatedResponse(bool succeeded, IEnumerable<TData> data, IEnumerable<string> messages, int count = 0, int page = 1, int pageSize = 15)
    {
        Succeeded = succeeded;
        Data = data;
        Messages = messages;
        TotalCount = count;
        CurrentPage = page;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

    }

    /// <summary>
    /// A failed response
    /// </summary>
    /// <returns>An empty instance of <typeparamref name="TData"/></returns>
    public static PaginatedResponse<TData> Failure()
    {
        return new PaginatedResponse<TData>(false, new List<TData>(), new List<string>());
    }

    /// <summary>
    /// A failed response
    /// </summary>
    /// <param name="message">The error message to add</param>
    /// <returns>An empty instance of <typeparamref name="TData"/></returns>
    public static PaginatedResponse<TData> Failure(string message)
    {
        return new PaginatedResponse<TData>(false, new List<TData>(), new List<string> { message });
    }

    /// <summary>
    /// A failed response
    /// </summary>
    /// <param name="messages">The error messages to add</param>
    /// <returns>An empty instance of <typeparamref name="TData"/></returns>
    public static PaginatedResponse<TData> Failure(IEnumerable<string> messages)
    {
        return new PaginatedResponse<TData>(false, new List<TData>(), messages);
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
        return new PaginatedResponse<TData>(true, data, new List<string>(), count, page, pageSize);
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
