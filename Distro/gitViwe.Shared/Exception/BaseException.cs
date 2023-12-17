namespace gitViwe.Shared;

/// <summary>
/// The base class for custom exceptions
/// </summary>
public abstract class BaseException : System.Exception
{
    /// <summary>
    /// Create a new instance of <see cref="BaseException"/>
    /// </summary>
    /// <param name="detail">An explanation specific to this occurrence of the problem.</param>
    /// <param name="message">The exception message.</param>
    public BaseException(string message, string detail)
        : base(message)
    {
        Detail = detail;
    }

    /// <summary>
    /// Create a new instance of <see cref="BaseException"/>
    /// </summary>
    /// <param name="detail">An explanation specific to this occurrence of the problem.</param>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception object.</param>
    public BaseException(string message, System.Exception innerException, string detail)
        : base(message, innerException)
    {
        Detail = detail;
    }

    /// <summary>
    /// An explanation specific to this occurrence of the problem.
    /// </summary>
    public string Detail { get; }
}
