namespace gitViwe.Shared.Imgbb.Extension;

internal static partial class LoggerExtension
{
    [LoggerMessage(
        EventId = default,
        Level = LogLevel.Warning,
        Message = "Upload image to ImgBB failed. {ResponseJSON}")]
    internal static partial void UploadImageFailed(this ILogger logger, string responseJSON);
}
