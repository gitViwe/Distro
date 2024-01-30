namespace gitViwe.Shared.Sqids.MediatR.Extension;

internal static partial class LoggerExtension
{
    /// <summary>
    /// Failed to validate the JSON Web Token.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="encoded">The encoded Sqids id.</param>
    /// <param name="decoded">The decoded Sqids id.</param>
    /// <param name="targetPropertyName">The target property name</param>
    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Decoded Sqids id and set property value. {encoded} {decoded} {targetPropertyName}")]
    public static partial void SqidsIdDecoded(this ILogger logger, string encoded, int decoded, string targetPropertyName);
}
