namespace gitViwe.Shared.ProblemDetail;

/// <summary>
/// A custom factory to produce <see cref="DefaultProblemDetails"/> and <see cref="DefaultValidationProblemDetails"/>.
/// </summary>
public static class ProblemDetailFactory
{
    /// <summary>
    /// Creates a <see cref="DefaultProblemDetails"/> instance that configures defaults for <br></br>
    /// <see cref="DefaultProblemDetails.Status"/><br></br>
    /// <see cref="DefaultProblemDetails.Type"/><br></br>
    /// <see cref="DefaultProblemDetails.Title"/><br></br>
    /// </summary>
    /// <param name="statusCode">The value for <see cref="DefaultProblemDetails.Status"/>.</param>
    /// <param name="instance">The value for <see cref="DefaultProblemDetails.Instance" />.</param>
    /// <param name="detail">The value for <see cref="DefaultProblemDetails.Detail" />.</param>
    /// <returns>A custom <see cref="IDefaultProblemDetails"/> class</returns>
    public static IDefaultProblemDetails CreateProblemDetails(int statusCode, string instance, string? detail = null)
    {
        var problemDetails = Create(statusCode, detail, instance);
        return new DefaultProblemDetails(Activity.Current?.Id, problemDetails);
    }

    /// <summary>
    /// Creates a <see cref="DefaultProblemDetails"/> instance that configures defaults for <br></br>
    /// <see cref="DefaultProblemDetails.Status"/><br></br>
    /// <see cref="DefaultProblemDetails.Type"/><br></br>
    /// <see cref="DefaultProblemDetails.Title"/><br></br>
    /// </summary>
    /// <param name="statusCode">The value for <see cref="DefaultProblemDetails.Status"/>.</param>
    /// <param name="instance">The value for <see cref="DefaultProblemDetails.Instance" />.</param>
    /// <param name="extensions">The object extension associated with this instance of <see cref="DefaultProblemDetails"/></param>
    /// <param name="detail">The value for <see cref="DefaultProblemDetails.Detail" />.</param>
    /// <returns>A custom <see cref="IDefaultProblemDetails"/> class</returns>
    public static IDefaultProblemDetails CreateProblemDetails(int statusCode, string instance, IDictionary<string, object?> extensions, string? detail = null)
    {
        var problemDetails = Create(statusCode, detail, instance);
        return new DefaultProblemDetails(Activity.Current?.Id, problemDetails, extensions);
    }

    /// <summary>
    /// Creates a <see cref="DefaultValidationProblemDetails"/> instance that configures defaults for <br></br>
    /// <see cref="DefaultProblemDetails.Status"/><br></br>
    /// <see cref="DefaultProblemDetails.Type"/><br></br>
    /// <see cref="DefaultProblemDetails.Title"/><br></br>
    /// </summary>
    /// <param name="statusCode">The value for <see cref="DefaultProblemDetails.Status"/>.</param>
    /// <param name="instance">The value for <see cref="DefaultProblemDetails.Instance" />.</param>
    /// <param name="errors">The errors associated with this instance of <see cref="DefaultValidationProblemDetails"/></param>
    /// <param name="detail">The value for <see cref="DefaultProblemDetails.Detail" />.</param>
    /// <returns>A custom <see cref="IValidationProblemDetails"/> class</returns>
    public static IValidationProblemDetails CreateValidationProblemDetails(int statusCode, string instance, IDictionary<string, string[]> errors, string? detail = null)
    {
        return Create(statusCode, errors, detail, instance);
    }

    private static DefaultProblemDetails Create(
        int statusCode,
        string? detail = null,
        string? instance = null)
    {
        var result = StatusCodeProblemDetails.Create(statusCode);
        SetProblemDefaults(result, detail, instance);
        return result;
    }

    private static DefaultValidationProblemDetails Create(
        int statusCode,
        IDictionary<string, string[]> errors,
        string? detail = null,
        string? instance = null)
    {
        var problem = StatusCodeProblemDetails.Create(statusCode);
        var result = new DefaultValidationProblemDetails(Activity.Current?.Id, problem, errors);
        SetProblemDefaults(result, detail, instance);
        return result;
    }

    private static void SetProblemDefaults(
            DefaultProblemDetails result,
            string? detail = null,
            string? instance = null)
    {
        if (!string.IsNullOrWhiteSpace(detail))
        {
            result.Detail = detail;
        }

        if (!string.IsNullOrWhiteSpace(instance))
        {
            result.Instance = instance;
        }
    }
}