﻿namespace gitViwe.Shared.Imgbb.Implementation;

internal sealed class LocalMockClient : IImgBbClient
{
    private static async Task<string> GetBase64StringFromFormFileAsync(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        byte[] fileBytes = memoryStream.ToArray();
        string base64String = Convert.ToBase64String(fileBytes);

        return $"data:{file.ContentType};base64," + base64String;
    }

    private static async Task<string> GetBase64StringFromHttpContentAsync(HttpContent httpContent)
    {
        using var memoryStream = new MemoryStream();
        await httpContent.CopyToAsync(memoryStream);

        byte[] fileBytes = memoryStream.ToArray();
        string base64String = Convert.ToBase64String(fileBytes);

        return $"data:{httpContent.Headers.ContentType?.MediaType};base64," + base64String;
    }

    public Task<IResponse> PingAsync(CancellationToken cancellation = default)
    {
        return Task.FromResult(Response.Success("ImgBB server integration is online."));
    }

    public async Task<ImgBbUploadResponse> UploadImageAsync(HttpContent httpContent, string fileName, int? expirationInSeconds = null, CancellationToken cancellation = default)
    {
        string base64 = await GetBase64StringFromHttpContentAsync(httpContent);

        return new ImgBbUploadResponse()
        {
            Status = 200,
            Success = true,
            Data = new ImgBBData()
            {
                DisplayUrl = base64,
                Expiration = expirationInSeconds ?? 15552000,
                Image = new ImgBBImage
                {
                    Filename = fileName,
                    Name = fileName,
                    Url = base64,
                },
                Thumb = new ImgBBThumb
                {
                    Filename = fileName,
                    Name = fileName,
                    Url = base64,
                }
            }
        };
    }

    public async Task<ImgBbUploadResponse> UploadImageAsync(IFormFile file, int? expirationInSeconds = null, CancellationToken cancellation = default)
    {
        string base64 = await GetBase64StringFromFormFileAsync(file);

        return new ImgBbUploadResponse()
        {
            Status = 200,
            Success = true,
            Data = new ImgBBData()
            {
                DisplayUrl = base64,
                Expiration = expirationInSeconds ?? 15552000,
                Image = new ImgBBImage
                {
                    Filename = file.Name,
                    Name = file.Name,
                    Url = base64,
                },
                Thumb = new ImgBBThumb
                {
                    Filename = file.Name,
                    Name = file.Name,
                    Url = base64,
                }
            }
        };
    }
}
