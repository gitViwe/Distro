## gitViwe.Shared.Sqids

### Nuget package:
```
dotnet add package gitViwe.Shared.Sqids 
```

#### Register the `ISqidsIdEncoder<T>` service
```csharp
builder.Services.AddGitViweSqidsIdEncoder();

// OR
public class MyCustomImplementation : ISqidsIdEncoder<T> {}

builder.Services.AddGitViweSqidsIdEncoder<MyCustomImplementation>();
```
#### Add configuration options to the `appsettings.json` file
```
{
  "SqidsIdEncoderOption": {
    "Alphabet": "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789",
    "MinLength": 6
  }
}
```
### Usage:

```csharp
static class ISqidsIdEncoder<T> {
    string Encode(T number);
    string Encode(params T[] numbers);
    string Encode(IEnumerable<T> numbers);
    bool TryDecode(ReadOnlySpan<char> id, out IReadOnlyList<T> decoded);
    bool TryDecode(ReadOnlySpan<char> id, out T decoded);
}
```