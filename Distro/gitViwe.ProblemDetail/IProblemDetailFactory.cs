using gitViwe.ProblemDetail.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gitViwe.ProblemDetail;

/// <summary>
/// A custom factory to produce <see cref="ProblemDetails"/> and <see cref="ValidationProblemDetails"/>.
/// </summary>
public interface IProblemDetailFactory
{
    /// <summary>
    /// Creates a <see cref="ProblemDetails" /> instance that configures defaults based on values specified in <see cref="ApiBehaviorOptions" />.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext" />.</param>
    /// <param name="statusCode">The value for <see cref="ProblemDetails.Status"/>.</param>
    /// <param name="detail">The value for <see cref="ProblemDetails.Detail" />.</param>
    /// <returns>A custom <see cref="ProblemDetails"/> class</returns>
    DefaultProblemDetails CreateProblemDetails(HttpContext context, int statusCode, string? detail = null);

    /// <summary>
    /// Creates a <see cref="ValidationProblemDetails" /> instance that configures defaults based on values specified in <see cref="ApiBehaviorOptions" />.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext" />.</param>
    /// <param name="errors">The <see cref="IDictionary{string, string[]}" />.</param>
    /// <param name="statusCode">The value for <see cref="ProblemDetails.Status"/>.</param>
    /// <param name="detail">The value for <see cref="ProblemDetails.Detail" />.</param>
    /// <returns>The <see cref="ValidationProblemDetails"/> instance.</returns>
    ValidationProblemDetails CreateValidationProblemDetails(HttpContext context, int statusCode, IDictionary<string, string[]> errors, string? detail = null);
}