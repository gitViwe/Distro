## gitViwe.Shared.JsonWebToken

### Nuget package:
```
dotnet add package gitViwe.Shared.JsonWebToken 
```

### JSON Web Token:
#### Register the `IJsonWebToken` service
```csharp
builder.Services.AddGitViweJsonWebToken(builder.Configuration);
```

#### Add configuration options to the `appsettings.json` file
```
{
  "JsonWebTokenOption": {
    "Issuer": "https://localhost",
    "Secret": "vxL2V6EEj8HjgU6NxMhcNWAf0Ejxmcuj",
    "ValidIssuers": ["https://localhost", "https://localhost"],
    "ValidAudiences": ["https://localhost", "https://localhost"],
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