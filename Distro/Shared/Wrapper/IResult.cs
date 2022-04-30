namespace Shared;

/// <summary>
/// A unified return type for the API endpoint
/// </summary>
public interface IResult
{
    /// <summary>
    /// The response messages
    /// </summary>
    IEnumerable<string> Messages { get; set; }

    /// <summary>
    /// Flags whether the process was successful
    /// </summary>
    bool Succeeded { get; set; }
}

/// <summary>
/// Extends on <see cref="IResult"/> to return data
/// </summary>
/// <typeparam name="TData">The data type returned from the request</typeparam>
public interface IResult<out TData> : IResult
{
    /// <summary>
    /// The content returned from the request
    /// </summary>
    TData Data { get; }
}
