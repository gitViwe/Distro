namespace gitViwe.Shared;

/// <summary>
/// A custom exception when a resource is not found
/// </summary>
public class NotFoundException : System.Exception
{
    /// <summary>
    /// Create a new instance of <see cref="NotFoundException"/>
    /// </summary>
    /// <param name="detail">An explanation specific to this occurrence of the problem.</param>
    /// <param name="message">The exception message.</param>
    public NotFoundException(string message, string? detail = null)
        : base(message)
    {
        Detail = detail;
    }

    /// <summary>
    /// Create a new instance of <see cref="NotFoundException"/>
    /// </summary>
    /// <param name="detail">An explanation specific to this occurrence of the problem.</param>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception object.</param>
    public NotFoundException(string message, System.Exception innerException, string? detail = null)
        : base(message, innerException)
    {
        Detail = detail;
    }

    /// <summary>
    /// An explanation specific to this occurrence of the problem.
    /// </summary>
    public string? Detail { get; }
}
