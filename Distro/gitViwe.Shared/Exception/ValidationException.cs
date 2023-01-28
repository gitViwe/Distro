namespace gitViwe.Shared;

/// <summary>
/// A custom exception when validation fails
/// </summary>
public class ValidationException : System.Exception
{
    private readonly IDictionary<string, string[]> _errors;

    /// <summary>
    /// Create a new instance of <see cref="ValidationException"/>
    /// </summary>
    /// <param name="errors">The key value pair of the errors.</param>
    public ValidationException(IDictionary<string, string[]> errors)
        : base("One or more validation failures have occurred.")
    {
        _errors = errors;
    }

    /// <summary>
    /// Create a new instance of <see cref="ValidationException"/>
    /// </summary>
    /// <param name="errors">The key value pair of the errors.</param>
    /// <param name="innerException">The inner exception object.</param>
    public ValidationException(IDictionary<string, string[]> errors, System.Exception innerException)
        : base("One or more validation failures have occurred.", innerException)
    {
        _errors = errors;
    }

    /// <summary>
    /// The key value pair of the errors.
    /// </summary>
    /// <returns>A dictionary keyed by property name
	/// where each value is an array of error messages associated with that property.</returns>
    public IDictionary<string, string[]> ToDictionary() => _errors;

    /// <summary>
    /// A collection of the validation errors without the key.
    /// </summary>
    public IEnumerable<string> ErrorMessages() => _errors.SelectMany(x => x.Value).ToArray();
}
