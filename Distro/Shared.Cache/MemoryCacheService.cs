using Microsoft.Extensions.Caching.Memory;

namespace Shared.Cache;

internal class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly MemoryCacheEntryOptions _cacheOptions = new();

    // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-6.0#options-interfaces
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

    public Task<TResult> GetAsync<TResult>(string key)
    {
        return Task.FromResult(_memoryCache.Get<TResult>(key));
    }

    public Task RemoveAsync(string key)
    {
        return Task.Run(() => _memoryCache.Remove(key));
    }

    public Task<TData> SetAsync<TData>(string key, TData data)
    {
        return Task.FromResult(_memoryCache.Set(key, data, _cacheOptions));
    }

    public Task<bool> TryGetAsync<TResult>(string key, out TResult data)
    {
        _memoryCache.TryGetValue(key, out data);

        return Task.FromResult(data is not null);
    }
}
