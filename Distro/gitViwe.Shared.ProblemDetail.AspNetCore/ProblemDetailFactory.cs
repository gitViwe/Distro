using BaseProblemDetailFactory = gitViwe.Shared.ProblemDetail.ProblemDetailFactory;

namespace gitViwe.Shared.ProblemDetail.AspNetCore;

/// <summary>
/// A custom factory to produce <see cref="DefaultProblemDetails"/> and <see cref="DefaultValidationProblemDetails"/>.
/// </summary>
public static class ProblemDetailFactory
{
    private const string CONTENT_TYPE = "application/problem+json";

    /// <summary>
    /// Creates a <see cref="DefaultProblemDetails"/> instance that configures defaults for <br></br>
    /// <see cref="DefaultProblemDetails.Status"/><br></br>
    /// <see cref="DefaultProblemDetails.Type"/><br></br>
    /// <see cref="DefaultProblemDetails.Title"/><br></br>
    /// <see cref="HttpResponse.ContentType"/><br></br>
    /// <see cref="HttpResponse.StatusCode"/>
    /// </summary>
    /// <param name="context">The <see cref="HttpContext" />.</param>
    /// <param name="statusCode">The value for <see cref="DefaultProblemDetails.Status"/>.</param>
    /// <param name="detail">The value for <see cref="DefaultProblemDetails.Detail" />.</param>
    /// <returns>A custom <see cref="DefaultProblemDetails"/> class</returns>
    public static IDefaultProblemDetails CreateProblemDetails(HttpContext context, int statusCode, string? detail = null)
    {
        context.Response.ContentType = CONTENT_TYPE;
        context.Response.StatusCode = statusCode;

        return BaseProblemDetailFactory.CreateProblemDetails(statusCode, context.Request.Path, detail);
    }

    /// <summary>
    /// Creates a <see cref="IResult"/> instance that configures defaults for <br></br>
    /// <see cref="DefaultProblemDetails.Status"/><br></br>
    /// <see cref="DefaultProblemDetails.Type"/><br></br>
    /// <see cref="DefaultProblemDetails.Title"/><br></br>
    /// <see cref="HttpResponse.ContentType"/><br></br>
    /// <see cref="HttpResponse.StatusCode"/>
    /// </summary>
    /// <param name="context">The <see cref="HttpContext" />.</param>
    /// <param name="statusCode">The value for <see cref="DefaultProblemDetails.Status"/>.</param>
    /// <param name="detail">The value for <see cref="DefaultProblemDetails.Detail" />.</param>
    /// <returns>A <see cref="IResult"/> class</returns>
    public static IResult CreateProblemResult(HttpContext context, int statusCode, string? detail = null)
    {
        IDefaultProblemDetails problem = CreateProblemDetails(context, statusCode, detail);

        return Results.Problem(problem.Detail, problem.Instance, problem.Status, problem.Title, problem.Type, problem.Extensions);
    }

    /// <summary>
    /// Creates a <see cref="DefaultProblemDetails"/> instance that configures defaults for <br></br>
    /// <see cref="DefaultProblemDetails.Status"/><br></br>
    /// <see cref="DefaultProblemDetails.Type"/><br></br>
    /// <see cref="DefaultProblemDetails.Title"/><br></br>
    /// <see cref="HttpResponse.ContentType"/><br></br>
    /// <see cref="HttpResponse.StatusCode"/>
    /// </summary>
    /// <param name="context">The <see cref="HttpContext" />.</param>
    /// <param name="statusCode">The value for <see cref="DefaultProblemDetails.Status"/>.</param>
    /// <param name="extensions">The object extension associated with this instance of <see cref="DefaultProblemDetails"/></param>
    /// <param name="detail">The value for <see cref="DefaultProblemDetails.Detail" />.</param>
    /// <returns>A custom <see cref="DefaultProblemDetails"/> class</returns>
    public static IDefaultProblemDetails CreateProblemDetails(HttpContext context, int statusCode, IDictionary<string, object?> extensions, string? detail = null)
    {
        context.Response.ContentType = CONTENT_TYPE;
        context.Response.StatusCode = statusCode;

        return BaseProblemDetailFactory.CreateProblemDetails(statusCode, context.Request.Path, extensions, detail);
    }

    /// <summary>
    /// Creates a <see cref="IResult"/> instance that configures defaults for <br></br>
    /// <see cref="DefaultProblemDetails.Status"/><br></br>
    /// <see cref="DefaultProblemDetails.Type"/><br></br>
    /// <see cref="DefaultProblemDetails.Title"/><br></br>
    /// <see cref="HttpResponse.ContentType"/><br></br>
    /// <see cref="HttpResponse.StatusCode"/>
    /// </summary>
    /// <param name="context">The <see cref="HttpContext" />.</param>
    /// <param name="statusCode">The value for <see cref="DefaultProblemDetails.Status"/>.</param>
    /// <param name="extensions">The object extension associated with this instance of <see cref="DefaultProblemDetails"/></param>
    /// <param name="detail">The value for <see cref="DefaultProblemDetails.Detail" />.</param>
    /// <returns>A <see cref="IResult"/> class</returns>
    public static IResult CreateProblemResult(HttpContext context, int statusCode, IDictionary<string, object?> extensions, string? detail = null)
    {
        IDefaultProblemDetails problem = CreateProblemDetails(context, statusCode, extensions, detail);

        return Results.Problem(problem.Detail, problem.Instance, problem.Status, problem.Title, problem.Type, problem.Extensions);
    }

    /// <summary>
    /// Creates a <see cref="DefaultValidationProblemDetails"/> instance that configures defaults for <br></br>
    /// <see cref="DefaultProblemDetails.Status"/><br></br>
    /// <see cref="DefaultProblemDetails.Type"/><br></br>
    /// <see cref="DefaultProblemDetails.Title"/><br></br>
    /// <see cref="HttpResponse.ContentType"/><br></br>
    /// <see cref="HttpResponse.StatusCode"/>
    /// </summary>
    /// <param name="context">The <see cref="HttpContext" />.</param>
    /// <param name="statusCode">The value for <see cref="DefaultProblemDetails.Status"/>.</param>
    /// <param name="errors">The errors associated with this instance of <see cref="DefaultValidationProblemDetails"/></param>
    /// <param name="detail">The value for <see cref="DefaultProblemDetails.Detail" />.</param>
    /// <returns>A custom <see cref="DefaultValidationProblemDetails"/> class</returns>
    public static IValidationProblemDetails CreateValidationProblemDetails(HttpContext context, int statusCode, IDictionary<string, string[]> errors, string? detail = null)
    {
        context.Response.ContentType = CONTENT_TYPE;
        context.Response.StatusCode = statusCode;

        return BaseProblemDetailFactory.CreateValidationProblemDetails(statusCode, context.Request.Path, errors, detail);
    }

    /// <summary>
    /// Creates a <see cref="IResult"/> instance that configures defaults for <br></br>
    /// <see cref="DefaultProblemDetails.Status"/><br></br>
    /// <see cref="DefaultProblemDetails.Type"/><br></br>
    /// <see cref="DefaultProblemDetails.Title"/><br></br>
    /// <see cref="HttpResponse.ContentType"/><br></br>
    /// <see cref="HttpResponse.StatusCode"/>
    /// </summary>
    /// <param name="context">The <see cref="HttpContext" />.</param>
    /// <param name="statusCode">The value for <see cref="DefaultProblemDetails.Status"/>.</param>
    /// <param name="errors">The errors associated with this instance of <see cref="DefaultValidationProblemDetails"/></param>
    /// <param name="detail">The value for <see cref="DefaultProblemDetails.Detail" />.</param>
    /// <returns>A <see cref="IResult"/> class</returns>
    public static IResult CreateValidationProblemResult(HttpContext context, int statusCode, IDictionary<string, string[]> errors, string? detail = null)
    {
        IValidationProblemDetails problem = CreateValidationProblemDetails(context, statusCode, errors, detail);

        return Results.ValidationProblem(problem.Errors, problem.Detail, problem.Instance, problem.Status, problem.Title, problem.Type, problem.Extensions);
    }
}