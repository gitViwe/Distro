﻿namespace gitViwe.Shared.Cache.Option;

/// <summary>
/// Configuration options for Redis.
/// </summary>
public class RedisDistributedCacheOption : RedisCacheOptions
{
    /// <summary>
    /// Gets or sets an absolute expiration time, relative to now. Default value is 1.
    /// </summary>
    public int AbsoluteExpirationInMinutes { get; set; } = 1;
    /// <summary>
    /// Gets or sets how long a cache entry can be inactive (e.g. not accessed) before it will be removed.
    /// This will not extend the entry lifetime beyond the absolute expiration (if set). Default value is 1.
    /// </summary>
    public int SlidingExpirationInMinutes { get; set; } = 1;
}

internal class RedisDistributedCacheOptionValidator : IValidateOptions<RedisDistributedCacheOption>
{
    public ValidateOptionsResult Validate(string? name, RedisDistributedCacheOption options)
    {
        if (options.AbsoluteExpirationInMinutes < 1)
        {
            return ValidateOptionsResult.Fail($"{name}.{nameof(options.AbsoluteExpirationInMinutes)} must be greater than zero.");
        }

        if (options.SlidingExpirationInMinutes < 1)
        {
            return ValidateOptionsResult.Fail($"{name}.{nameof(options.SlidingExpirationInMinutes)} must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(options.InstanceName))
        {
            return ValidateOptionsResult.Fail($"A value for {name}.{nameof(options.InstanceName)} must be provided.");
        }

        return ValidateOptionsResult.Success;
    }
}