## gitViwe.Shared.TimeBasedOneTimePassword

### Nuget package:
```
dotnet add package gitViwe.Shared.TimeBasedOneTimePassword
```

#### Register the `ITimeBasedOneTimePassword`

```csharp
builder.Services.AddGitViweTimeBasedOneTimePassword(options => {
    options.Issuer = "My awesome app";
    options.Algorithm = TimeBasedOneTimePasswordAlgorithm.SHA1;
    options.Digits = 6;
    options.Period = 30;
});
```
### Usage:

```csharp
static class ITimeBasedOneTimePassword {
    bool VerifyToken(string secretKey, string token);
    TimeBasedOneTimePasswordLinkResponse? GenerateLink(string username);
}
```