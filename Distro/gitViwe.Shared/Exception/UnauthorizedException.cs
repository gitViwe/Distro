namespace gitViwe.Shared;

/// <summary>
/// A custom exception when a request lacks valid authentication credentials
/// </summary>
public sealed class UnauthorizedException : BaseException
{
    /// <summary>
    /// Create a new instance of <see cref="UnauthorizedException"/>
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="detail">An explanation specific to this occurrence of the problem.</param>
    public UnauthorizedException(string message, string detail = "You are not Authorized.")
        : base(message, detail) { }

    /// <summary>
    /// Create a new instance of <see cref="UnauthorizedException"/>
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception object.</param>
    /// <param name="detail">An explanation specific to this occurrence of the problem.</param>
    public UnauthorizedException(string message, System.Exception innerException, string detail = "You are not Authorized.")
        : base(message, innerException, detail) { }
}
