namespace gitViwe.Shared;

/// <summary>
/// A unified return type for the API endpoint
/// </summary>
public class Response : IResponse
{
    /// <summary>
    /// The response messages
    /// </summary>
    public IEnumerable<string> Messages { get; set; } = new List<string>();

    /// <summary>
    /// Flags whether the process was successful
    /// </summary>
    public bool Succeeded { get; set; }

    /// <summary>
    /// Unsuccessful result
    /// </summary>
    /// <returns>The <see cref="Succeeded"/> property value</returns>
    public static IResponse Fail()
    {
        return new Response { Succeeded = false };
    }

    /// <summary>
    /// Unsuccessful result
    /// </summary>
    /// <param name="message">The error message to add</param>
    /// <returns>The <see cref="Succeeded"/> and <see cref="Messages"/> property values</returns>
    public static IResponse Fail(string message)
    {
        return new Response { Succeeded = false, Messages = new List<string> { message } };
    }

    /// <summary>
    /// Unsuccessful result
    /// </summary>
    /// <param name="messages">The error messages to add</param>
    /// <returns>The <see cref="Succeeded"/> and <see cref="Messages"/> property values</returns>
    public static IResponse Fail(IEnumerable<string> messages)
    {
        return new Response { Succeeded = false, Messages = messages };
    }

    /// <summary>
    /// Successful result
    /// </summary>
    /// <returns>The <see cref="Succeeded"/> property value</returns>
    public static IResponse Success()
    {
        return new Response { Succeeded = true };
    }

    /// <summary>
    /// Successful result
    /// </summary>
    /// <param name="message">The success message to add</param>
    /// <returns>The <see cref="Succeeded"/> and <see cref="Messages"/> property values</returns>
    public static IResponse Success(string message)
    {
        return new Response { Succeeded = true, Messages = new List<string> { message } };
    }

    /// <summary>
    /// Successful result
    /// </summary>
    /// <param name="messages">The error messages to add</param>
    /// <returns>The <see cref="Succeeded"/> and <see cref="Messages"/> property values</returns>
    public static IResponse Success(IEnumerable<string> messages)
    {
        return new Response { Succeeded = true, Messages = messages };
    }
}

/// <summary>
/// Extends on <see cref="Response"/> to return data
/// </summary>
/// <typeparam name="TData">The data type returned from the request</typeparam>
public class Response<TData> : IResponse<TData> where TData : class, new()
{
    /// <summary>
    /// The content returned from the request
    /// </summary>
    public TData Data { get; set; } = new();

    /// <summary>
    /// The response messages
    /// </summary>
    public IEnumerable<string> Messages { get; set; } = new List<string>();

    /// <summary>
    /// Flags whether the process was successful
    /// </summary>
    public bool Succeeded { get; set; }

    /// <summary>
    /// Unsuccessful result
    /// </summary>
    /// <param name="data">The content to add</param>
    /// <returns>The <see cref="Succeeded"/> property value</returns>
    public static IResponse<TData> Fail(TData data)
    {
        return new Response<TData> { Succeeded = false, Data = data };
    }

    /// <summary>
    /// Unsuccessful result
    /// </summary>
    /// <param name="message">The error message to add</param>
    /// <param name="data">The content to add</param>
    /// <returns>The <see cref="Succeeded"/> and <see cref="Messages"/> property values</returns>
    public static IResponse<TData> Fail(string message, TData data)
    {
        return new Response<TData> { Succeeded = false, Messages = new List<string> { message }, Data = data };
    }

    /// <summary>
    /// Unsuccessful result
    /// </summary>
    /// <param name="messages">The error messages to add</param>
    /// <param name="data">The content to add</param>
    /// <returns>The <see cref="Succeeded"/> and <see cref="Messages"/> property values</returns>
    public static IResponse<TData> Fail(IEnumerable<string> messages, TData data)
    {
        return new Response<TData> { Succeeded = false, Messages = messages, Data = data };
    }

    /// <summary>
    /// Successful result
    /// </summary>
    /// <param name="data">The content to add</param>
    /// <returns>The <see cref="Data"/> property value</returns>
    public static IResponse<TData> Success(TData data)
    {
        return new Response<TData> { Succeeded = true, Data = data };
    }

    /// <summary>
    /// Successful result
    /// </summary>
    /// <param name="message">The success message to add</param>
    /// <param name="data">The content to add</param>
    /// <returns>The <see cref="Succeeded"/> and <see cref="Messages"/> property values</returns>
    public static IResponse<TData> Success(string message, TData data)
    {
        return new Response<TData> { Succeeded = true, Messages = new List<string> { message }, Data = data };
    }

    /// <summary>
    /// Successful result
    /// </summary>
    /// <param name="messages">The success messages to add</param>
    /// <param name="data">The content to add</param>
    /// <returns>The <see cref="Data"/> and <see cref="Messages"/> property values</returns>
    public static IResponse<TData> Success(IEnumerable<string> messages, TData data)
    {
        return new Response<TData> { Succeeded = true, Data = data, Messages = messages };
    }
}
