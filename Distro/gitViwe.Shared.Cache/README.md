## gitViwe.Shared.Cache

### Nuget package:
```
dotnet add package gitViwe.Shared.Cache 
```

### Redis distributed cache:
#### Register the `IRedisDistributedCache` service using by specifying the settings values
```
builder.Services.AddGitViweRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName= "redis_demo";
    options.AbsoluteExpirationInMinutes = 5;
    options.SlidingExpirationInMinutes = 2;
});
```

### Usage:

```csharp
interface IRedisDistributedCache {
    TResult? Get<TResult>(string key);
    string? Get(string key);
    Task<TResult?> GetAsync<TResult>(string key, CancellationToken token = default);
    Task<string?> GetAsync(string key, CancellationToken token = default);
    void Set<TValue>(string key, TValue value, TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null);
    Task SetAsync<TValue>(string key, TValue value, TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null, CancellationToken token = default);
    void Refresh(string key);
    Task RefreshAsync(string key, CancellationToken token = default);
    void Remove(string key);
    Task RemoveAsync(string key, CancellationToken token = default);
}
```