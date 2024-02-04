## gitViwe.Shared.OpenTelemetry.MediatR

### Nuget package:
```
dotnet add package gitViwe.Shared.OpenTelemetry.MediatR
```
### Pipeline
Some custom pipeline classes
```csharp
class OpenTelemetryPreProcessor<TRequest> { }
class OpenTelemetryPostProcessor<TRequest, TResponse> { }
```

### Register options:
#### Register the `OpenTelemetryBehaviourOption` used by `OpenTelemetryPreProcessor<TRequest>`
```csharp
builder.Services.ConfigureGitViweOpenTelemetryBehaviourOption(options =>
{
    options.ObfuscatedPropertyNames = [ "Password", "PasswordConfirmation", "Token" ];
});
```