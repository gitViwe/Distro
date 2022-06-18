using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace gitViwe.Shared.Cache;

internal class RedisCacheService : IRedisCacheService
{
    private readonly IDistributedCache _distributedCache;
    private readonly DistributedCacheEntryOptions _cacheOptions = new();

    public RedisCacheService(
        IDistributedCache distributedCache,
        IOptionsMonitor<CacheConfiguration> cacheConfiguration)
    {
        _distributedCache = distributedCache;
        if (cacheConfiguration.CurrentValue is not null)
        {
            _cacheOptions = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddHours(cacheConfiguration.CurrentValue.AbsoluteExpirationInHours),
                SlidingExpiration = TimeSpan.FromMinutes(cacheConfiguration.CurrentValue.SlidingExpirationInMinutes),
            };
        }
    }

    public async Task<TResult> GetAsync<TResult>(string key, CancellationToken token = default) where TResult : class, new()
    {
        // get the cached item
        var byteValue = await _distributedCache.GetAsync(key, token);

        if (byteValue is null)
        {
            return new TResult();
        }
        
        var stringValue = Encoding.UTF8.GetString(byteValue);
        var data = JsonSerializer.Deserialize<TResult>(stringValue);

        return data ?? new TResult();
    }

    public async Task RemoveAsync(string key, CancellationToken token = default)
    {
        // remove the cached item
        await _distributedCache.RemoveAsync(key, token);
    }

    public async Task SetAsync<TData>(string key, TData data, CancellationToken token = default)
    {
        // add the item to cache
        var stringValue = JsonSerializer.Serialize(data);

        var byteValue = Encoding.UTF8.GetBytes(stringValue);

        await _distributedCache.SetAsync(key, byteValue, _cacheOptions, token);
    }
}
