## gitViwe.Shared.ProblemDetail.Extension

### Nuget package:
```
dotnet add package gitViwe.Shared.ProblemDetail.Extension 
```

Some helpful extension methods
```csharp
class ResponseExtension {
    static async Task<IDefaultProblemDetails?> ToProblemResponseAsync(
        this HttpResponseMessage response,
        JsonSerializerOptions? options = null,
        CancellationToken token = default);
    static async Task<IValidationProblemDetails?> ToValidationProblemResponseAsync(
        this HttpResponseMessage response,
        JsonSerializerOptions? options = null,
        CancellationToken token = default);
}
```