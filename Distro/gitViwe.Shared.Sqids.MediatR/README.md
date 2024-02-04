## gitViwe.Shared.Sqids.MediatR

### Nuget package:
```
dotnet add package gitViwe.Shared.Sqids.MediatR
```

### Attribute:

Some custom attributes
```csharp
class DecodedSqidsIdAttribute { }
```

### Pipeline
Some custom pipeline classes
```csharp
class SqidsIdEncoderPreProcessor<TRequest> { }
```

### Usage:
Register the pipeline through MediatR and decorate your command / query class.
```csharp
services.AddGitViweSqidsIdEncoder()
        .AddMediatR(config =>
        {
            config.AddOpenRequestPreProcessor(typeof(SqidsIdEncoderPreProcessor<>));
        });
```
```csharp
class MediatrCommand : IRequest<MediatrResponse>, ISqidsIdEncoderPreProcessMarker
{
    public required string UserId { get; init; }

    [DecodedSqidsId(SourceProperty = nameof(UserId))]
    public int DecodedUserId { get; set; }
}
```