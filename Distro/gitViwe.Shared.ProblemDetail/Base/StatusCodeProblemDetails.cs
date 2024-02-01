namespace gitViwe.Shared.ProblemDetail.Base;

/// <summary>
/// A basic problem details representation for an HTTP status code.
/// It includes default values for <see cref="DefaultProblemDetails.Status"/>, <see cref="DefaultProblemDetails.Type"/> and <see cref="DefaultProblemDetails.Title"/>.
/// </summary>
internal class StatusCodeProblemDetails
{
    /// <summary>
    /// Creates a <see cref="DefaultProblemDetails"/> class based on the <paramref name="statusCode"/>
    /// </summary>
    /// <param name="statusCode">The HTTP status code</param>
    /// <returns>A <see cref="DefaultProblemDetails"/> class with default value for
    /// <see cref="DefaultProblemDetails.Status"/>, <see cref="DefaultProblemDetails.Type"/> and <see cref="DefaultProblemDetails.Title"/>.</returns>
    internal static DefaultProblemDetails Create(int statusCode)
    {
        var details = new DefaultProblemDetails();

        SetDetails(details, statusCode);

        return details;
    }

    private static void SetDetails(DefaultProblemDetails details, int statusCode)
    {
        details.Status = statusCode;
        details.Type = GetDefaultType(statusCode);
        details.Title = ReasonPhrases.GetReasonPhrase(statusCode);
    }

    internal static string GetDefaultType(int statusCode)
    {
        return $"https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/{statusCode}";
    }
}
