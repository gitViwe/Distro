using gitViwe.ProblemDetail.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

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
    /// Process the HTTP response message into the <see cref="DefaultProblemDetails"/>
    /// </summary>
    /// <param name="response">The HTTP response message from the API</param>
    /// <param name="options">The serializer options</param>
    /// <param name="token">The cancellation token</param>
    /// <returns>The content of an HTTP response message as a <see cref="DefaultProblemDetails"/></returns>
    public static async Task<DefaultProblemDetails> ToProblemResponseAsync(
        this HttpResponseMessage response,
        JsonSerializerOptions? options = null,
        CancellationToken token = default)
    {
        var problem = await response.Content.ReadFromJsonAsync<DefaultProblemDetails>(options ?? _serializerOptions, token);

        return problem!;
    }

    /// <summary>
    /// Process the HTTP response message into the <see cref="ValidationProblemDetails"/>
    /// </summary>
    /// <param name="response">The HTTP response message from the API</param>
    /// <param name="options">The serializer options</param>
    /// <param name="token">The cancellation token</param>
    /// <returns>The content of an HTTP response message as a <see cref="ValidationProblemDetails"/></returns>
    public static async Task<ValidationProblemDetails> ToValidationProblemResponseAsync(
        this HttpResponseMessage response,
        JsonSerializerOptions? options = null,
        CancellationToken token = default)
    {
        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>(options ?? _serializerOptions, token);

        return problem!;
    }
}
