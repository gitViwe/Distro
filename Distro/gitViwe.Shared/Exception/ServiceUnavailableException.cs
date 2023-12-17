namespace gitViwe.Shared;

/// <summary>
/// A custom exception when a service is unavailable
/// </summary>
public class ServiceUnavailableException : BaseException
{
    /// <summary>
    /// Create a new instance of <see cref="NotFoundException"/>
    /// </summary>
    /// <param name="detail">An explanation specific to this occurrence of the problem.</param>
    /// <param name="message">The exception message.</param>
    public ServiceUnavailableException(string message, string detail = "Service unavailable.")
        : base(message, detail) { }

    /// <summary>
    /// Create a new instance of <see cref="NotFoundException"/>
    /// </summary>
    /// <param name="detail">An explanation specific to this occurrence of the problem.</param>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception object.</param>
    public ServiceUnavailableException(string message, System.Exception innerException, string detail = "Service unavailable.")
        : base(message, innerException, detail) { }
}
