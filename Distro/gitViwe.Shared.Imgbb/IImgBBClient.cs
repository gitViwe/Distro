using gitViwe.Shared.Imgbb.Contract;
using Microsoft.AspNetCore.Http;

namespace gitViwe.Shared.Imgbb;

/// <summary>
/// An abstraction of an image hosting service using Imgbb
/// </summary>
public interface IImgBBClient
{
    /// <summary>
    /// Upload the image file.
    /// </summary>
    /// <param name="httpContent">The content body to upload</param>
    /// <param name="fileName">The full name of the image file</param>
    /// <param name="expirationInSeconds">The expiration time in seconds</param>
    /// <param name="cancellation">Propagates notification that operations should be cancelled</param>
    /// <returns>A <seealso cref="ImgBBUploadResponse"/> instance with the upload details</returns>
    Task<ImgBBUploadResponse> UploadImageAsync(HttpContent httpContent, string fileName, int? expirationInSeconds = null, CancellationToken cancellation = default);

    /// <summary>
    /// Upload the image file.
    /// </summary>
    /// <param name="file">The file to upload</param>
    /// <param name="expirationInSeconds">The expiration time in seconds</param>
    /// <param name="cancellation">Propagates notification that operations should be cancelled</param>
    /// <returns></returns>
    Task<ImgBBUploadResponse> UploadImageAsync(IFormFile file, int? expirationInSeconds = null, CancellationToken cancellation = default);
}