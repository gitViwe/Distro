## gitViwe.Shared.JsonWebToken

### Nuget package:
```
dotnet add package gitViwe.Shared.JsonWebToken 
```

### JSON Web Token:
#### Register the `IJsonWebToken` service
```csharp
builder.Services.AddGitViweJsonWebToken();
```

#### Add configuration options to the `appsettings.json` file
```
{
  "JsonWebTokenOption": {
    "ExpiryInSeconds": 60,
    "Issuer": "https://localhost",
    "Secret": "vxL2V6EEj8HjgU6NxMhcNWAf0Ejxmcuj",
    "ValidIssuers": ["https://localhost"],
    "ValidAudiences": ["https://localhost"],
    "ValidateAudience": true,
    "ValidateIssuer": true
  }
}
```

### Usage:

```csharp
IJsonWebToken {
    string CreateJsonWebToken(IEnumerable<Claim> claims, string audience);
    Task<ClaimsPrincipal?> ValidateToken(string token, bool isRefreshToken = false);
}
```