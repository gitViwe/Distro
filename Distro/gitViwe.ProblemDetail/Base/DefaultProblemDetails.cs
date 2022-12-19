using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json.Serialization;

namespace gitViwe.ProblemDetail.Base;

/// <summary>
/// The base implementation for creating a custom <see cref="ProblemDetails"/> class
/// </summary>
public class DefaultProblemDetails : ProblemDetails
{
    /// <summary>
    /// Creates a custom <see cref="ProblemDetails"/> class
    /// </summary>
    /// <param name="traceIdentifier">Theunique identifier to represent this request in trace logs</param>
    /// <param name="problemDetails">The default <see cref="ProblemDetails"/></param>
    public DefaultProblemDetails(string traceIdentifier, ProblemDetails problemDetails)
    {
        SetProblemDefaults(problemDetails);
        TraceId = traceIdentifier;
    }

    /// <summary>
    /// A unique identifier to represent this request in trace logs
    /// </summary>
    [JsonPropertyName("traceId")]
    public string TraceId { get; }

    /// <summary>
    /// A string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the object.</returns>
    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"Type    : {Type}");
        stringBuilder.AppendLine($"Title   : {Title}");
        stringBuilder.AppendLine($"Status  : {Status}");
        stringBuilder.AppendLine($"Detail  : {Detail}");
        stringBuilder.AppendLine($"Instance: {Instance}");

        return stringBuilder.ToString();
    }

    private void SetProblemDefaults(ProblemDetails problem)
    {
        this.Status = problem.Status;
        this.Title = problem.Title;
        this.Type = problem.Type;
        this.Detail = problem.Detail;
        this.Instance = problem.Instance;
        foreach (var item in problem.Extensions)
        {
            this.Extensions.Add(item);
        }
    }
}
