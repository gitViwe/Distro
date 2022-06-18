using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace gitViwe.Shared.Cache;

internal class MemoryCacheService : IMemoryCacheService
{
    private readonly MemoryCacheEntryOptions _cacheOptions = new();
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheService(
        IMemoryCache memoryCache,
        IOptionsMonitor<CacheConfiguration> cacheConfiguration)
    {
        _memoryCache = memoryCache;
        if (cacheConfiguration.CurrentValue is not null)
        {
            _cacheOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddHours(cacheConfiguration.CurrentValue.AbsoluteExpirationInHours),
                SlidingExpiration = TimeSpan.FromMinutes(cacheConfiguration.CurrentValue.SlidingExpirationInMinutes),
                Priority = CacheItemPriority.High,
            };
        }
    }

    public Task<TResult> GetAsync<TResult>(string key) where TResult : class, new()
    {
        var data = _memoryCache.Get<TResult>(key);

        return Task.FromResult(data ?? new TResult());
    }

    public Task RemoveAsync(string key)
    {
        _memoryCache.Remove(key);

        return Task.CompletedTask;
    }

    public Task SetAsync<TData>(string key, TData data)
    {
        _memoryCache.Set(key, data, _cacheOptions);

        return Task.CompletedTask;
    }
}
