namespace gitViwe.Shared.Cache.Implementation;

internal sealed class DefaultRedisDistributedCache(
    IDistributedCache distributedCache,
    IOptions<RedisDistributedCacheOption> options,
    ILogger<DefaultRedisDistributedCache> logger) : IRedisDistributedCache
{
    private readonly RedisDistributedCacheOption _cacheOption = options.Value;

    private DistributedCacheEntryOptions CreateCacheEntryOptions(TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null) =>
        new()
        {
            AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow ?? TimeSpan.FromSeconds(_cacheOption.AbsoluteExpirationInSeconds),
            SlidingExpiration = slidingExpiration ?? TimeSpan.FromSeconds(_cacheOption.SlidingExpirationInSeconds),
        };

    public TResult? Get<TResult>(string key)
    {
        var byteValue = distributedCache.Get(key);

        if (byteValue is null)
        {
            logger.FailedToRetrieveCacheItem(key);
            return default;
        }

        var stringValue = Encoding.UTF8.GetString(byteValue);
        var data = JsonSerializer.Deserialize<TResult>(stringValue);

        return data;
    }

    public string? Get(string key)
    {
        var byteValue = distributedCache.Get(key);

        if (byteValue is null)
        {
            logger.FailedToRetrieveCacheItem(key);
            return null;
        }

        return Encoding.UTF8.GetString(byteValue);
    }

    public async Task<TResult?> GetAsync<TResult>(string key, CancellationToken token = default)
    {
        var byteValue = await distributedCache.GetAsync(key, token);

        if (byteValue is null)
        {
            logger.FailedToRetrieveCacheItem(key);
            return default;
        }

        var stringValue = Encoding.UTF8.GetString(byteValue);
        return JsonSerializer.Deserialize<TResult>(stringValue);
    }

    public async Task<string?> GetAsync(string key, CancellationToken token = default)
    {
        var byteValue = await distributedCache.GetAsync(key, token);

        if (byteValue is null)
        {
            logger.FailedToRetrieveCacheItem(key);
            return null;
        }

        return Encoding.UTF8.GetString(byteValue);
    }

    public void Refresh(string key)
    {
        distributedCache.Refresh(key);
    }

    public Task RefreshAsync(string key, CancellationToken token = default)
    {
        return distributedCache.RefreshAsync(key, token);
    }

    public void Remove(string key)
    {
        distributedCache.Remove(key);
    }

    public Task RemoveAsync(string key, CancellationToken token = default)
    {
        return distributedCache.RemoveAsync(key, token);
    }

    public void Set<TValue>(string key, TValue value, TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null)
    {
        var stringValue = JsonSerializer.Serialize(value);

        var byteValue = Encoding.UTF8.GetBytes(stringValue);

        distributedCache.Set(key, byteValue, CreateCacheEntryOptions(absoluteExpirationRelativeToNow, slidingExpiration));
    }

    public void Set(string key, string value, TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null)
    {
        var byteValue = Encoding.UTF8.GetBytes(value);

        distributedCache.Set(key, byteValue, CreateCacheEntryOptions(absoluteExpirationRelativeToNow, slidingExpiration));
    }

    public Task SetAsync<TValue>(string key, TValue value, TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null, CancellationToken token = default)
    {
        var stringValue = JsonSerializer.Serialize(value);

        var byteValue = Encoding.UTF8.GetBytes(stringValue);

        return distributedCache.SetAsync(key, byteValue, CreateCacheEntryOptions(absoluteExpirationRelativeToNow, slidingExpiration), token);
    }

    public Task SetAsync(string key, string value, TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null, CancellationToken token = default)
    {
        var byteValue = Encoding.UTF8.GetBytes(value);

        return distributedCache.SetAsync(key, byteValue, CreateCacheEntryOptions(absoluteExpirationRelativeToNow, slidingExpiration), token);
    }
}
