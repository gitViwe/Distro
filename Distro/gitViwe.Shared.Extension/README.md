## gitViwe.Shared.Extension

### Nuget package:
```
dotnet add package gitViwe.Shared.Extension 
```

Some helpful extension methods
```csharp
class ClaimsPrincipalExtension {
    static bool HasExpiredClaims(this ClaimsPrincipal claimsPrincipal, int thresholdInMinutes = 5);
}

class ResponseExtension {
    static Task<TData?> ToResponseAsync<TData>(
        this HttpResponseMessage response,
        JsonSerializerOptions? options = null,
        CancellationToken token = default);
    static Task<PaginatedResponse<TData>?> ToPaginatedResponseAsync<TData>(
        this HttpResponseMessage response,
        JsonSerializerOptions? options = null,
        CancellationToken token = default);
}

class ServiceCollectionExtension {
    static OptionsBuilder<TOptions> AddOptionsWithValidateOnStart<TOptions, TValidateOptions>(
        this IServiceCollection services,
        string name,
        Action<TOptions> options);
}

class ServiceCollectionExtension {
    static bool IsDocker(this IHostEnvironment environment);
    static bool IsTest(this IHostEnvironment environment);
    static bool IsAny(this IHostEnvironment environment, IEnumerable<string> environmentNames);
}
```