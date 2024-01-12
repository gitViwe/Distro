using gitViwe.Shared.Imgbb.Contract;
using gitViwe.Shared.Imgbb.Option;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Net.Http.Json;

namespace gitViwe.Shared.Imgbb;

internal class ImgBBClient : IImgBBClient
{
    private readonly HttpClient _httpClient;
    private readonly ImgBBClientOption _options;
    private readonly string _uploadEndpoint;

    public ImgBBClient(HttpClient httpClient, IOptionsMonitor<ImgBBClientOption> options)
    {
        _httpClient = httpClient;
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

        result.EnsureSuccessStatusCode();

        var response = await result.Content.ReadFromJsonAsync<ImgBBUploadResponse>(cancellationToken: cancellation);

        return response!;
    }
}
