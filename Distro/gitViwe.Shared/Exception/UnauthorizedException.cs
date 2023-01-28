namespace gitViwe.Shared;

/// <summary>
/// A custom exception when a request lacks valid authentication credentials
/// </summary>
public class UnauthorizedException : System.Exception
{
    /// <summary>
    /// Create a new instance of <see cref="UnauthorizedException"/>
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="detail">An explanation specific to this occurrence of the problem.</param>
    public UnauthorizedException(string message, string? detail = null)
        : base(message)
    {
        Detail = detail;
    }

    /// <summary>
    /// Create a new instance of <see cref="UnauthorizedException"/>
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception object.</param>
    /// <param name="detail">An explanation specific to this occurrence of the problem.</param>
    public UnauthorizedException(string message, System.Exception innerException, string? detail = null)
        : base(message, innerException)
    {
        Detail = detail;
    }

    /// <summary>
    /// An explanation specific to this occurrence of the problem.
    /// </summary>
    public string? Detail { get; }
}
