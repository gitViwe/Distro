namespace gitViwe.Shared.Cache.Extension;

internal static partial class RedisDistributedCacheLoggerExtension
{
    /// <summary>
    /// Failed to get cache item value.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="key">A string identifying the requested value.</param>
    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Failed to get cache item value with key: {Key}")]
    internal static partial void FailedToRetrieveCacheItem(this ILogger logger, string key);
}
