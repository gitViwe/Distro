namespace gitViwe.Shared;

/// <summary>
/// A custom exception when a request has insufficient rights to a resource
/// </summary>
public class ForbiddenException : BaseException
{
    /// <summary>
    /// Create a new instance of <see cref="ForbiddenException"/>
    /// </summary>
    /// <param name="detail">An explanation specific to this occurrence of the problem.</param>
    /// <param name="message">The exception message.</param>
    public ForbiddenException(string message, string detail = "You are not authorized to access this resource.")
        : base(message, detail) { }

    /// <summary>
    /// Create a new instance of <see cref="ForbiddenException"/>
    /// </summary>
    /// <param name="detail">An explanation specific to this occurrence of the problem.</param>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception object.</param>
    public ForbiddenException(string message, System.Exception innerException, string detail = "You are not authorized to access this resource.")
        : base(message, innerException, detail) { }
}
