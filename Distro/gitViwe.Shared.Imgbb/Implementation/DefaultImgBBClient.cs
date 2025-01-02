namespace gitViwe.Shared.Imgbb.Implementation;

internal sealed class DefaultImgBbClient(
    HttpClient httpClient,
    IOptions<ImgBbClientOption> options,
    ILogger<DefaultImgBbClient> logger) : IImgBbClient
{
    private readonly ImgBbClientOption _imgBbClientOption = options.Value;

    public Task<ImgBbUploadResponse> UploadImageAsync(IFormFile file, int? expirationInSeconds = null, CancellationToken cancellation = default)
    {
        return UploadImageAsync(new StreamContent(file.OpenReadStream()), file.FileName, expirationInSeconds, cancellation);
    }

    public async Task<ImgBbUploadResponse> UploadImageAsync(HttpContent httpContent, string fileName, int? expirationInSeconds = null, CancellationToken cancellation = default)
    {
        var endpoint = $"1/upload?key={_imgBbClientOption.ApiKey}&expiration={expirationInSeconds ?? _imgBbClientOption.ExpirationInSeconds}";

        var content = new MultipartFormDataContent
        {
            { httpContent, "image", fileName }
        };

        var result = await httpClient.PostAsync(endpoint, content, cancellation);

        if (result.IsSuccessStatusCode)
        {
            var response = await result.Content.ReadFromJsonAsync<ImgBbUploadResponse>(cancellationToken: cancellation);

            return response!;
        }

        logger.UploadImageFailed(await result.Content.ReadAsStringAsync(cancellation));

        return new ImgBbUploadResponse { Success = false };
    }

    public async Task<IResponse> PingAsync(CancellationToken cancellation = default)
    {
        var endpoint = $"1/upload?key={_imgBbClientOption.ApiKey}&expiration=15";

        byte[] image = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAABEAAAASCAIAAAAym6IDAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAACVSURBVDhPY/hPOhjVQ2U9v/YffG/pAEQ/Vq2FCsEATj1A1W9lVIDonbI2VAgGcOr5UlQO0QNEUCEYwG2PoSXJeuAagOhb70SoKBgQpQeIfl+5BpXAowfodWQ9n5MzoRJ49HxtaEHWA0TfpsyASOHU8+/Hj08xSch64IGOUw8QALWhuRAijk8PEADdA9fwtbIWJPT/PwDmpjgaAgZw6QAAAABJRU5ErkJggg==");

        var content = new MultipartFormDataContent
        {
            { new ByteArrayContent(image), "image", "test-image" }
        };

        var result = await httpClient.PostAsync(endpoint, content, cancellation);

        if (result.IsSuccessStatusCode)
        {
            return Response.Success("ImgBB server integration is online.");
        }

        var response = await result.Content.ReadFromJsonAsync<ImgBBClientPingErrorResponse>(cancellationToken: cancellation);

        return Response.Fail(response!.Error.Message);
    }
}
