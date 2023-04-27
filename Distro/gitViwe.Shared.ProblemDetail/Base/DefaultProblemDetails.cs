using gitViwe.Shared.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json.Serialization;

namespace gitViwe.ProblemDetail.Base;

/// <summary>
/// The base implementation for creating a custom <see cref="ProblemDetails"/> class
/// </summary>
public class DefaultProblemDetails : ProblemDetails, IDefaultProblemDetails
{
    /// <summary>
    /// Creates a custom <see cref="ProblemDetails"/> class
    /// </summary>
    public DefaultProblemDetails() { }

    /// <summary>
    /// Creates a custom <see cref="ProblemDetails"/> class
    /// </summary>
    /// <param name="traceIdentifier">The unique identifier to represent this request in trace logs</param>
    /// <param name="problemDetails">The default <see cref="ProblemDetails"/></param>
    public DefaultProblemDetails(string traceIdentifier, ProblemDetails problemDetails)
    {
        SetProblemDefaults(problemDetails);
        TraceId = traceIdentifier;
    }

    /// <summary>
    /// Creates a custom <see cref="ProblemDetails"/> class
    /// </summary>
    /// <param name="traceIdentifier">The unique identifier to represent this request in trace logs</param>
    /// <param name="problemDetails">The default <see cref="ProblemDetails"/></param>
    /// <param name="extensions">Problem type definitions MAY extend the problem details object with additional members.</param>
    public DefaultProblemDetails(string traceIdentifier, ProblemDetails problemDetails, IDictionary<string, object?> extensions)
        :this(traceIdentifier, problemDetails)
    {
        AddExtensions(extensions);
    }

    /// <summary>
    /// A unique identifier to represent this request in trace logs
    /// </summary>
    [JsonPropertyName("traceId")]
    public string? TraceId { get; init; }

    /// <summary>
    /// A string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the object.</returns>
    public override string? ToString()
    {
        var stringBuilder = new StringBuilder()
            .AppendLine($"TraceId : {TraceId}")
            .AppendLine($"Type    : {Type}")
            .AppendLine($"Title   : {Title}")
            .AppendLine($"Status  : {Status}")
            .AppendLine($"Detail  : {Detail}")
            .AppendLine($"Instance: {Instance}");

        return stringBuilder.ToString();
    }

    private void SetProblemDefaults(ProblemDetails problem)
    {
        Status = problem.Status;
        Title = problem.Title;
        Type = problem.Type;

        if (!string.IsNullOrWhiteSpace(problem.Detail))
        {
            Detail = problem.Detail;
        }

        if (!string.IsNullOrWhiteSpace(problem.Instance))
        {
            Instance = problem.Instance;
        }
    }

    private void AddExtensions(IDictionary<string, object?> extensions)
    {
        if (extensions is not null && extensions.Any())
        {
            foreach (var item in extensions)
            {
                Extensions.Add(item);
            }
        }
    }
}
