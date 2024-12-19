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
