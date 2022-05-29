using Microsoft.Extensions.Caching.Memory;

namespace Shared.Cache;

internal class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly MemoryCacheEntryOptions _cacheOptions = new();

    public MemoryCacheService(IMemoryCache memoryCache, CacheConfiguration cacheConfiguration)
    {
        _memoryCache = memoryCache;
        if (cacheConfiguration is not null)
        {
            _cacheOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddHours(cacheConfiguration.AbsoluteExpirationInHours),
                SlidingExpiration = TimeSpan.FromMinutes(cacheConfiguration.SlidingExpirationInMinutes),
                Priority = CacheItemPriority.High,
            };
        }
    }

    public async ValueTask<TResult> GetAsync<TResult>(string key, CancellationToken token = default) where TResult : class, new()
    {
        // get the cached item
        var data = await Task.Run(() => _memoryCache.Get<TResult>(key), token);

        if (data is null)
        {
            return new TResult();
        }

        return data;
    }

    public async ValueTask RemoveAsync(string key, CancellationToken token = default)
    {
        // remove the cached item
        await Task.Run(() => _memoryCache.Remove(key), token);
    }

    public async ValueTask SetAsync<TData>(string key, TData data, CancellationToken token = default)
    {
        // add the item to cache
        await Task.Run(() => _memoryCache.Set(key, data, _cacheOptions), token);
    }
}
