using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Shared.Cache;

internal class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;
    private readonly DistributedCacheEntryOptions _cacheOptions = new();

    public RedisCacheService(IDistributedCache distributedCache, CacheConfiguration cacheConfiguration)
    {
        _distributedCache = distributedCache;
        if (cacheConfiguration is not null)
        {
            _cacheOptions = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddHours(cacheConfiguration.AbsoluteExpirationInHours),
                SlidingExpiration = TimeSpan.FromMinutes(cacheConfiguration.SlidingExpirationInMinutes),
            };
        }
    }

    public async ValueTask<TResult> GetAsync<TResult>(string key, CancellationToken token = default) where TResult : class, new()
    {
        // get the cached item
        var byteValue = await _distributedCache.GetAsync(key, token);

        if (byteValue is null)
        {
            return new TResult();
        }
        
        var stringValue = Encoding.UTF8.GetString(byteValue);
        var data = JsonSerializer.Deserialize<TResult>(stringValue);

        if (data is null)
        {
            return new TResult();
        }

        return data;
    }

    public async ValueTask RemoveAsync(string key, CancellationToken token = default)
    {
        // remove the cached item
        await _distributedCache.RemoveAsync(key, token);
    }

    public async ValueTask SetAsync<TData>(string key, TData data, CancellationToken token = default)
    {
        // add the item to cache
        var stringValue = JsonSerializer.Serialize(data);

        var byteValue = Encoding.UTF8.GetBytes(stringValue);

        await _distributedCache.SetAsync(key, byteValue, _cacheOptions, token);
    }
}
