## gitViwe.Shared.Cache

### Nuget package:
```
dotnet add package gitViwe.Shared.Cache 
```

### Redis distributed cache:
#### Register the `IRedisDistributedCache` service
```csharp
builder.Services.AddGitViweRedisCache(builder.Configuration);
```

#### Add configuration options to the `appsettings.json` file
```
{
  "RedisDistributedCacheOption": {
    "Configuration": "localhost:6379",
    "InstanceName": "redis_demo",
    "AbsoluteExpirationInSeconds": 300,
    "SlidingExpirationInSeconds": 120
  }
}
```

### Usage:

```csharp
interface IRedisDistributedCache {
    TResult? Get<TResult>(string key);
    string? Get(string key);
    Task<TResult?> GetAsync<TResult>(string key, CancellationToken token = default);
    Task<string?> GetAsync(string key, CancellationToken token = default);
    void Set(string key, string value, TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null);
    void Set<TValue>(string key, TValue value, TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null);
    Task SetAsync(string key, string value, TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null, CancellationToken token = default);
    Task SetAsync<TValue>(string key, TValue value, TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null, CancellationToken token = default);
    void Refresh(string key);
    Task RefreshAsync(string key, CancellationToken token = default);
    void Remove(string key);
    Task RemoveAsync(string key, CancellationToken token = default);
}
```