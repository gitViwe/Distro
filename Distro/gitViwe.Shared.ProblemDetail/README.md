## Problem Details

### Nuget package:
```
dotnet add package gitViwe.Shared.ProblemDetail 
```

### Usage:

```csharp
static class ProblemDetailFactory {
    static DefaultProblemDetails CreateProblemDetails(int statusCode, string instance, string? detail = null);
    static DefaultProblemDetails CreateProblemDetails(int statusCode, string instance, IDictionary<string, object?> extensions, string? detail = null);
    static DefaultValidationProblemDetails CreateValidationProblemDetails(int statusCode, string instance, IDictionary<string, string[]> errors, string? detail = null);
}
```