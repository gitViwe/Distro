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
    static async Task<TData> ToResponseAsync<TData>(
        this HttpResponseMessage response,
        JsonSerializerOptions? options = null,
        CancellationToken token = default);
    static async Task<PaginatedResponse<TData>> ToPaginatedResponseAsync<TData>(
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
```