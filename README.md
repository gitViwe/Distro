# Distro
This repository will house a collection of shared libraries that will be distributed in NuGet that should help quickly launch projects I would like to play around with.

## Problem Details

### Nuget package:
```
dotnet add package gitViwe.ProblemDetail --version 1.1.2
```

### Usage:

```csharp
public IActionResult Result()
{
    var extensionValue = new
    {
        Balance = 30.0m,
        Accounts = { "/account/12345", "/account/67890" }
    };

    var problem = ProblemDetailFactory.CreateProblemDetails(
                    context: HttpContext,
                    statusCode: StatusCodes.Status412PreconditionFailed,
                    extensions: new Dictionary<string, object?>()
                    {
                        { "outOfCredit", extensionValue }
                    },
                    detail: "Your current balance is 30, but that costs 50.");

    return StatusCode(problem.Status!.Value, problem);
}
```

## Caching

### Nuget package:
```
dotnet add package gitViwe.Shared.Cache --version 1.0.14
```

### Redis distributed cache:
#### Register the `IRedisDistributedCache` service using settings from configuration file or manually set values

```csharp
builder.Services.AddGitViweRedisCache(builder.Configuration)
```
```
builder.Services.AddGitViweRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName= "redis_demo";
    options.AbsoluteExpirationInMinutes = 5;
    options.SlidingExpirationInMinutes = 2;
});
```

#### Add configuration to `appsettings.json` file
```json
"ConnectionStrings": {
    "Redis": "localhost:6379"
  },
  "RedisDistributedCacheOption": {
    "InstanceName": "redis_demo",
    "AbsoluteExpirationInMinutes": 5,
    "SlidingExpirationInMinutes": 2
  }
```

### Usage:

```csharp
public IActionResult Result([FromServices] IRedisDistributedCache redis, [FromBody] UrlShortenRequest request)
{
    await redis.SetAsync(key: "mykey", value: request.Uri, absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(request.MinutesUntilExpiry));

    string value = await redis.GetAsync(key: "mykey") ?? string.Empty;

    return value;
}
```