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
    /// Flags whether the process resulted in a success <see cref="StatusCode"/>
    /// </summary>
    bool Succeeded => StatusCode is >= 200 and <= 299;
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
    TData? Data { get; }
}

/// <summary>
/// Extends on <see cref="IResponse"/> to return a validation error
/// </summary>
public interface IValidationErrorResponse : IResponse
{
    /// <summary>
    /// The key value pair of the errors.
    /// </summary>
    /// <returns>A dictionary keyed by property name
    /// where each value is an array of error messages associated with that property.</returns>
    IDictionary<string, string[]> Errors { get; }
}
