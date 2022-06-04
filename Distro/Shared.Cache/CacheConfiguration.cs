namespace Shared.Cache;

/// <summary>
/// Defines the cache settings from appsettings.json
/// </summary>
internal record CacheConfiguration
{
    public int SlidingExpirationInMinutes { get; init; } = 5;
    public int AbsoluteExpirationInHours { get; init; } = 1;
}