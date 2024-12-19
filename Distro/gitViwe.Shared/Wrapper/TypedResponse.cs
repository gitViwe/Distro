using gitViwe.Shared.Exception;

namespace gitViwe.Shared;

/// <summary>
/// Extends on <see cref="Response"/> to return data
/// </summary>
/// <typeparam name="TData">The data type returned from the request</typeparam>
public sealed class TypedResponse<TData> : ITypedResponse<TData>
{
    private TypedResponse(string message, int statusCode, TData? data)
    {
        Data = data;
        Message = message;
        StatusCode = statusCode;
    }

    /// <summary>
    /// The content returned from the request
    /// </summary>
    public TData? Data { get; }

    /// <summary>
    /// The response messages.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// The HTTP status code.
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    /// Creates a new <see cref="TypedResponse{TData}"/> instance.
    /// </summary>
    /// <param name="message">The message containing the failure details.</param>
    /// <returns>A new <see cref="TypedResponse{TData}"/> instance with the <seealso cref="TypedResponse{TData}.StatusCode"/> value of 400.</returns>
    public static ITypedResponse<TData> Fail(string message)
    {
        return new TypedResponse<TData>(message, 400, default);
    }

    /// <summary>
    /// Creates a new <see cref="TypedResponse{TData}"/> instance.
    /// </summary>
    /// <param name="message">The message containing the failure details.</param>
    /// <param name="statusCode">The HTTP status code relating to this failure.</param>
    /// <returns>A new <see cref="TypedResponse{TData}"/> instance.</returns>
    public static ITypedResponse<TData> Fail(string message, int statusCode)
    {
        GuardException.Against.SuccessStatusCode(statusCode);
        return new TypedResponse<TData>(message, statusCode, default);
    }

    /// <summary>
    /// Creates a new <see cref="TypedResponse{TData}"/> instance.
    /// </summary>
    /// <param name="message">The message containing the success details.</param>
    /// <param name="data">The content to add.</param>
    /// <returns>A new <see cref="TypedResponse{TData}"/> instance where the data is <typeparamref name="TData"/> with the <seealso cref="TypedResponse{TData}.StatusCode"/> value of 200.</returns>
    public static ITypedResponse<TData> Success(string message, TData data)
    {
        return new TypedResponse<TData>(message, 200, data);
    }

    /// <summary>
    /// Creates a new <see cref="TypedResponse{TData}"/> instance.
    /// </summary>
    /// <param name="message">The message containing the success details.</param>
    /// <param name="statusCode">The HTTP status code relating to this response.</param>
    /// <param name="data">The content to add.</param>
    /// <returns>A new <see cref="TypedResponse{TData}"/> instance where the data is <typeparamref name="TData"/> with the <seealso cref="TypedResponse{TData}.StatusCode"/> value of 200.</returns>
    public static ITypedResponse<TData> Success(string message, int statusCode, TData data)
    {
        GuardException.Against.ErrorStatusCode(statusCode);
        return new TypedResponse<TData>(message, statusCode, data);
    }
}