using gitViwe.Shared.Exception;

namespace gitViwe.Shared;

/// <summary>
/// A unified return type for the API endpoint.
/// </summary>
public class Response : IResponse
{
    internal Response(string message, int statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }

    /// <summary>
    /// The response messages.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// The HTTP status code.
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    /// Creates a new <see cref="Response"/> instance.
    /// </summary>
    /// <param name="message">The message containing the failure details.</param>
    /// <returns>A new <see cref="Response"/> instance with the <seealso cref="StatusCode"/> value of 400.</returns>
    public static IResponse Fail(string message)
    {
        return new Response(message, 400);
    }

    /// <summary>
    /// Creates a new <see cref="Response"/> instance.
    /// </summary>
    /// <param name="message">The message containing the failure details.</param>
    /// <param name="statusCode">The HTTP status code relating to this failure.</param>
    /// <returns>A new <see cref="Response"/> instance.</returns>
    public static IResponse Fail(string message, int statusCode)
    {
        GuardException.Against.SuccessStatusCode(statusCode);
        return new Response(message, statusCode);
    }

    /// <summary>
    /// Creates a new <see cref="Response"/> instance.
    /// </summary>
    /// <param name="message">The message containing the success details.</param>
    /// <returns>A new <see cref="Response"/> instance with the <seealso cref="StatusCode"/> value of 200.</returns>
    public static IResponse Success(string message)
    {
        return new Response(message, 200);
    }

    /// <summary>
    /// Creates a new <see cref="Response"/> instance.
    /// </summary>
    /// <param name="message">The message containing the success details.</param>
    /// <param name="statusCode">The HTTP status code relating to this response.</param>
    /// <returns>A new <see cref="Response"/> instance.</returns>
    public static IResponse Success(string message, int statusCode)
    {
        GuardException.Against.ErrorStatusCode(statusCode);
        return new Response(message, statusCode);
    }
}

/// <summary>
/// Extends on <see cref="Response"/> to return data
/// </summary>
/// <typeparam name="TData">The data type returned from the request</typeparam>
public class Response<TData> : Response, IResponse<TData> where TData : class, new()
{
    internal Response(string message, int statusCode, TData data)
        : base(message, statusCode)
    {
        Data = data;
    }

    /// <summary>
    /// The content returned from the request
    /// </summary>
    public TData Data { get; } = new();

    /// <summary>
    /// Creates a new <see cref="Response{TData}"/> instance.
    /// </summary>
    /// <param name="message">The message containing the failure details.</param>
    /// <returns>A new <see cref="Response{TData}"/> instance with the <seealso cref="Response.StatusCode"/> value of 400.</returns>
    public static new IResponse<TData> Fail(string message)
    {
        return new Response<TData>(message, 400, new());
    }

    /// <summary>
    /// Creates a new <see cref="Response{TData}"/> instance.
    /// </summary>
    /// <param name="message">The message containing the failure details.</param>
    /// <param name="statusCode">The HTTP status code relating to this failure.</param>
    /// <returns>A new <see cref="Response{TData}"/> instance..</returns>
    public static new IResponse<TData> Fail(string message, int statusCode)
    {
        GuardException.Against.SuccessStatusCode(statusCode);
        return new Response<TData>(message, statusCode, new());
    }

    /// <summary>
    /// Creates a new <see cref="Response{TData}"/> instance.
    /// </summary>
    /// <param name="message">The message containing the success details.</param>
    /// <param name="data">The content to add.</param>
    /// <returns>A new <see cref="Response{TData}"/> instance where the data is <typeparamref name="TData"/> with the <seealso cref="Response.StatusCode"/> value of 200.</returns>
    public static IResponse<TData> Success(string message, TData data)
    {
        return new Response<TData>(message, 200, data);
    }

    /// <summary>
    /// Creates a new <see cref="Response{TData}"/> instance.
    /// </summary>
    /// <param name="message">The message containing the success details.</param>
    /// <param name="statusCode">The HTTP status code relating to this response.</param>
    /// <param name="data">The content to add.</param>
    /// <returns>A new <see cref="Response{TData}"/> instance where the data is <typeparamref name="TData"/> with the <seealso cref="Response.StatusCode"/> value of 200.</returns>
    public static IResponse<TData> Success(string message, int statusCode, TData data)
    {
        GuardException.Against.ErrorStatusCode(statusCode);
        return new Response<TData>(message, statusCode, data);
    }
}
