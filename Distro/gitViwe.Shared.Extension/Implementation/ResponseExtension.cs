namespace gitViwe.Shared.Extension;

/// <summary>
/// Provides wrapper extensions for the the <see cref="HttpResponseMessage"/>
/// </summary>
public static class ResponseExtension
{
    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        ReferenceHandler = ReferenceHandler.Preserve,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    /// <summary>
    /// Process the HTTP response message into a <typeparamref name="TData"/>
    /// </summary>
    /// <typeparam name="TData">The data type returned from the response</typeparam>
    /// <param name="response">The HTTP response message from the API</param>
    /// <param name="options">The serializer options</param>
    /// <param name="token">The cancellation token</param>
    /// <returns>The content of an HTTP response message as a <typeparamref name="TData"/></returns>
    public static async Task<TData> ToResponseAsync<TData>(
        this HttpResponseMessage response,
        JsonSerializerOptions? options = null,
        CancellationToken token = default) where TData : class, new()
    {
        var responseObject = await response.Content.ReadFromJsonAsync<TData>(options ?? _serializerOptions, token);

        return responseObject ?? new();
    }

    /// <summary>
    /// Process the HTTP response message into the wrapper class <see cref="Response"/>
    /// </summary>
    /// <typeparam name="TData">The data type returned from the response</typeparam>
    /// <param name="response">The HTTP response message from the API</param>
    /// <param name="options">The serializer options</param>
    /// <param name="token">The cancellation token</param>
    /// <returns>The content of an HTTP response message as a <see cref="PaginatedResponse{TData}"/> model</returns>
    public static async Task<PaginatedResponse<TData>> ToPaginatedResponseAsync<TData>(
        this HttpResponseMessage response,
        JsonSerializerOptions? options = null,
        CancellationToken token = default) where TData : class, new()
    {
        var responseObject = await response.Content.ReadFromJsonAsync<PaginatedResponse<TData>>(options ?? _serializerOptions, token);

        return responseObject ?? PaginatedResponse<TData>.Fail();
    }
}
