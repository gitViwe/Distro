## gitViwe.Shared.Sqids

### Nuget package:
```
dotnet add package gitViwe.Shared.Sqids 
```

#### Register the `ISqidsIdEncoder<T>`
```csharp
builder.Services.AddGitViweSqidsIdEncoder(options => {
    options.Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    options.MinLength = 6;
});

// OR

builder.Services.AddGitViweSqidsIdEncoder<TImplementation>(options => {
    options.Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    options.MinLength = 6;
});
```
### Usage:

```csharp
static class ISqidsIdEncoder<T> {
    string Encode(T number);
    string Encode(params T[] numbers);
    string Encode(IEnumerable<T> numbers);
    IReadOnlyList<T> Decode(ReadOnlySpan<char> id);
    bool TryDecode(ReadOnlySpan<char> id, out IReadOnlyList<T> decoded);
    bool TryDecode(ReadOnlySpan<char> id, out T decoded);
}
```