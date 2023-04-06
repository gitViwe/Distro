namespace gitViwe.Shared;

/// <summary>
/// A unified return type for the API endpoint
/// </summary>
public interface IResponse
{
    /// <summary>
    /// The response messages
    /// </summary>
    string Message { get; }

    /// <summary>
    /// The HTTP status code
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    /// Flags whether the process was successful
    /// </summary>
    bool Succeeded => (StatusCode >= 200) && (StatusCode <= 299);
}

/// <summary>
/// Extends on <see cref="IResponse"/> to return data
/// </summary>
/// <typeparam name="TData">The data type returned from the request</typeparam>
public interface IResponse<out TData> : IResponse
{
    /// <summary>
    /// The content returned from the request
    /// </summary>
    TData Data { get; }
}
