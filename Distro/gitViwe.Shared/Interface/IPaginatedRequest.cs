namespace gitViwe.Shared;

/// <summary>
/// The default request to get a paginated response
/// </summary>
public interface IPaginatedRequest
{
    /// <summary>
    /// The current page number
    /// </summary>
    int CurrentPage { get; }

    /// <summary>
    /// The number of items in a single page
    /// </summary>
    int PageSize { get; }

    /// <summary>
    /// Create a query string with names of and values from <see cref="CurrentPage"/> and <see cref="PageSize"/>
    /// </summary>
    /// <returns></returns>
    string ToQueryParameterString() => $"?{nameof(CurrentPage)}={CurrentPage}&{nameof(PageSize)}={PageSize}";
}
