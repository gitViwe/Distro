namespace gitViwe.Shared;

/// <summary>
/// A unified return type for the API endpoint.
/// </summary>
public sealed class Response : IResponse
{
    private Response(string message, int statusCode)
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
    /// Creates a new <see cref="TypedResponse{TData}"/> instance.
    /// </summary>
    /// <param name="message">The message containing the failure details.</param>
    /// <returns>A new <see cref="TypedResponse{TData}"/> instance with the <seealso cref="TypedResponse{TData}.StatusCode"/> value of 400.</returns>
    public static ITypedResponse<TData> Fail<TData>(string message)
    {
        return TypedResponse<TData>.Fail(message);
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
    /// Creates a new <see cref="TypedResponse{TData}"/> instance.
    /// </summary>
    /// <param name="message">The message containing the failure details.</param>
    /// <param name="statusCode">The HTTP status code relating to this failure.</param>
    /// <returns>A new <see cref="TypedResponse{TData}"/> instance.</returns>
    public static ITypedResponse<TData> Fail<TData>(string message, int statusCode)
    {
        return TypedResponse<TData>.Fail(message, statusCode);
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
    /// Creates a new <see cref="TypedResponse{TData}"/> instance.
    /// </summary>
    /// <param name="message">The message containing the success details.</param>
    /// <param name="data">The content to add.</param>
    /// <returns>A new <see cref="TypedResponse{TData}"/> instance where the data is <typeparamref name="TData"/> with the <seealso cref="TypedResponse{TData}.StatusCode"/> value of 200.</returns>
    public static ITypedResponse<TData> Success<TData>(string message, TData data)
    {
        return TypedResponse<TData>.Success(message, data);
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
    
    /// <summary>
    /// Creates a new <see cref="TypedResponse{TData}"/> instance.
    /// </summary>
    /// <param name="message">The message containing the success details.</param>
    /// <param name="statusCode">The HTTP status code relating to this response.</param>
    /// <param name="data">The content to add.</param>
    /// <returns>A new <see cref="TypedResponse{TData}"/> instance where the data is <typeparamref name="TData"/> with the <seealso cref="TypedResponse{TData}.StatusCode"/> value of 200.</returns>
    public static ITypedResponse<TData> Success<TData>(string message, int statusCode, TData data)
    {
        return TypedResponse<TData>.Success(message, statusCode, data);
    }
}
