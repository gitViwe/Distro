namespace Shared.Cache;

internal class RedisCacheService : ICacheService
{
    public Task<TResult> GetAsync<TResult>(string key)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(string key)
    {
        throw new NotImplementedException();
    }

    public Task<TData> SetAsync<TData>(string key, TData data)
    {
        throw new NotImplementedException();
    }

    public Task<bool> TryGetAsync<TResult>(string key, out TResult data)
    {
        throw new NotImplementedException();
    }
}
