using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace gitViwe.Shared.Cache.Option;

/// <summary>
/// Configuration options for Redis.
/// </summary>
public sealed class RedisDistributedCacheOption : RedisCacheOptions
{
    /// <summary>
    /// The configuration values from the "RedisDistributedCacheOption" section inside the appsettings.json file.
    /// </summary>
    public const string SectionName = "RedisDistributedCacheOption";
    
    /// <summary>
    /// Sets an absolute expiration time, relative to now. Default value is 15.
    /// </summary>
    [Required]
    [DefaultValue(15)]
    [Range(14, int.MaxValue, ErrorMessage = "Please enter a value bigger than {14}")]
    public int AbsoluteExpirationInSeconds { get; init; }
    /// <summary>
    /// Sets how long a cache entry can be inactive (e.g. not accessed) before it will be removed.
    /// This will not extend the entry lifetime beyond the absolute expiration (if set). Default value is 15.
    /// </summary>
    [Required]
    [DefaultValue(15)]
    [Range(14, int.MaxValue, ErrorMessage = "Please enter a value bigger than {14}")]
    public int SlidingExpirationInSeconds { get; init; }
}