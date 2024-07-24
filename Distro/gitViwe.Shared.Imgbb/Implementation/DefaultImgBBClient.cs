namespace gitViwe.Shared.Imgbb;

internal sealed class DefaultImgBBClient : IImgBBClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<DefaultImgBBClient> _logger;
    private readonly ImgBBClientOption _options;
    private readonly string _uploadEndpoint;

    public DefaultImgBBClient(HttpClient httpClient, IOptionsMonitor<ImgBBClientOption> options, ILogger<DefaultImgBBClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _options = options.CurrentValue;
        _uploadEndpoint = $"1/upload?key={_options.APIKey}";
    }

    public Task<ImgBBUploadResponse> UploadImageAsync(IFormFile file, int? expirationInSeconds = null, CancellationToken cancellation = default)
    {
        return UploadImageAsync(new StreamContent(file.OpenReadStream()), file.FileName, expirationInSeconds, cancellation);
    }

    public async Task<ImgBBUploadResponse> UploadImageAsync(HttpContent httpContent, string fileName, int? expirationInSeconds = null, CancellationToken cancellation = default)
    {
        var endpoint = new StringBuilder()
            .Append(_uploadEndpoint);

        if (expirationInSeconds.HasValue || _options.ExpirationInSeconds.HasValue)
        {
            endpoint.Append($"&expiration={expirationInSeconds ?? _options.ExpirationInSeconds}");
        }

        var content = new MultipartFormDataContent
        {
            { httpContent, "image", fileName }
        };

        var result = await _httpClient.PostAsync(endpoint.ToString(), content, cancellation);

        if (result.IsSuccessStatusCode)
        {
            var response = await result.Content.ReadFromJsonAsync<ImgBBUploadResponse>(cancellationToken: cancellation);

            return response!;
        }

        _logger.UploadImageFailed(await result.Content.ReadAsStringAsync(cancellation));

        return new ImgBBUploadResponse { Success = false };
    }

    public async Task<IResponse> PingAsync(CancellationToken cancellation = default)
    {
        var endpoint = new StringBuilder()
            .Append(_uploadEndpoint)
            .Append("&expiration=60");

        byte[] image = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAABEAAAASCAIAAAAym6IDAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAACVSURBVDhPY/hPOhjVQ2U9v/YffG/pAEQ/Vq2FCsEATj1A1W9lVIDonbI2VAgGcOr5UlQO0QNEUCEYwG2PoSXJeuAagOhb70SoKBgQpQeIfl+5BpXAowfodWQ9n5MzoRJ49HxtaEHWA0TfpsyASOHU8+/Hj08xSch64IGOUw8QALWhuRAijk8PEADdA9fwtbIWJPT/PwDmpjgaAgZw6QAAAABJRU5ErkJggg==");

        var content = new MultipartFormDataContent
        {
            { new ByteArrayContent(image), "image", "test-image" }
        };

        var result = await _httpClient.PostAsync(endpoint.ToString(), content, cancellation);

        if (result.IsSuccessStatusCode)
        {
            return Response.Success("ImgBB server integration is online.");
        }

        var response = await result.Content.ReadFromJsonAsync<ImgBBClientPingErrorResponse>(cancellationToken: cancellation);

        return Response.Fail(response!.Error.Message);
    }
}
