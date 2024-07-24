namespace gitViwe.Shared.ProblemDetail.Base;

/// <summary>
/// The implementation for creating a custom <see cref="DefaultValidationProblemDetails"/> class
/// </summary>
public sealed class DefaultValidationProblemDetails : DefaultProblemDetails, IValidationProblemDetails
{
    /// <summary>
    /// Creates a custom <see cref="DefaultValidationProblemDetails"/> class
    /// </summary>
    public DefaultValidationProblemDetails() { }

    /// <summary>
    /// Creates a custom <see cref="DefaultValidationProblemDetails"/> class
    /// </summary>
    /// <param name="traceIdentifier">The unique identifier to represent this request in trace logs</param>
    /// <param name="problemDetails">The default <see cref="DefaultProblemDetails"/></param>
    /// <param name="errors">Gets the validation errors associated with this instance of HttpValidationProblemDetails.</param>
    public DefaultValidationProblemDetails(
        string? traceIdentifier,
        DefaultProblemDetails problemDetails,
        IDictionary<string, string[]> errors) : base(traceIdentifier, problemDetails)
    {
        AddErrors(errors);
        Title = "One or more validation errors occurred.";
    }

    /// <summary>
    /// Gets the validation errors associated with this instance of HttpValidationProblemDetails
    /// </summary>
    [JsonPropertyName("errors")]
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();

    private void AddErrors(IDictionary<string, string[]> errors)
    {
        if (errors is not null && errors.Any())
        {
            foreach (var item in errors)
            {
                Errors.Add(item);
            }
        }
    }
}
