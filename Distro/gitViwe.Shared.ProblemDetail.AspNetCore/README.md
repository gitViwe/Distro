## Problem Details AspNetCore

### Nuget package:
```
dotnet add package gitViwe.Shared.ProblemDetail.AspNetCore
```

### Usage:

```csharp
static class ProblemDetailFactory {
    static IResult CreateProblemResult(HttpContext context, int statusCode, string? detail = null);
    static DefaultProblemDetails CreateProblemDetails(HttpContext context, int statusCode, string? detail = null);
    static IResult CreateProblemResult(HttpContext context, int statusCode, IDictionary<string, object?> extensions, string? detail = null);
    static DefaultProblemDetails CreateProblemDetails(HttpContext context, int statusCode, IDictionary<string, object?> extensions, string? detail = null);
    static IResult CreateValidationProblemResult(HttpContext context, int statusCode, IDictionary<string, string[]> errors, string? detail = null);
    static DefaultValidationProblemDetails CreateValidationProblemDetails(HttpContext context, int statusCode, IDictionary<string, string[]> errors, string? detail = null);
}
```