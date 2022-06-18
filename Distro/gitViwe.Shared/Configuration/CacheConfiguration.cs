namespace gitViwe.Shared;

/// <summary>
/// Defines the cache settings from appsettings.json
/// </summary>
public record CacheConfiguration
{
    /// <summary>
    /// Gets or sets how long a cache entry can be inactive (e.g. not accessed) before
    /// it will be removed. This will not extend the entry lifetime beyond the absolute
    /// expiration (if set).
    /// </summary>
    public int SlidingExpirationInMinutes { get; init; } = 5;

    /// <summary>
    /// Gets or sets an absolute expiration date for the cache entry.
    /// </summary>
    public int AbsoluteExpirationInHours { get; init; } = 1;
}