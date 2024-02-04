## gitViwe.Shared.FluentValidation.MediatR

### Nuget package:
```
dotnet add package gitViwe.Shared.FluentValidation.MediatR
```

### Pipeline:

Some custom FluentValidation pipeline methods
```csharp
class FluentValidationPreProcessor<TRequest> { }
```

### Usage:
Register the pipeline through MediatR
```csharp
services.AddMediatR(config =>
        {
            config.AddOpenRequestPreProcessor(typeof(FluentValidationPreProcessor<>));
        });
```