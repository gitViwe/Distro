namespace gitViwe.Shared.Imgbb;

/// <summary>
/// An abstraction of an image hosting service using Imgbb
/// </summary>
public interface IImgBbClient
{
    /// <summary>
    /// Performs a test upload to ensure Imgbb integration is working.
    /// </summary>
    /// <param name="cancellation">Propagates notification that operations should be cancelled</param>
    /// <returns></returns>
    Task<IResponse> PingAsync(CancellationToken cancellation = default);

    /// <summary>
    /// Upload the image file.
    /// </summary>
    /// <param name="httpContent">The content body to upload</param>
    /// <param name="fileName">The full name of the image file</param>
    /// <param name="expirationInSeconds">The expiration time in seconds</param>
    /// <param name="cancellation">Propagates notification that operations should be cancelled</param>
    /// <returns>A <seealso cref="ImgBbUploadResponse"/> instance with the upload details</returns>
    Task<ImgBbUploadResponse> UploadImageAsync(HttpContent httpContent, string fileName, int? expirationInSeconds = null, CancellationToken cancellation = default);

    /// <summary>
    /// Upload the image file.
    /// </summary>
    /// <param name="file">The file to upload</param>
    /// <param name="expirationInSeconds">The expiration time in seconds</param>
    /// <param name="cancellation">Propagates notification that operations should be cancelled</param>
    /// <returns></returns>
    Task<ImgBbUploadResponse> UploadImageAsync(IFormFile file, int? expirationInSeconds = null, CancellationToken cancellation = default);
}