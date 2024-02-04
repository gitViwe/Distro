## gitViwe.Shared.JsonWebToken

### Nuget package:
```
dotnet add package gitViwe.Shared.JsonWebToken 
```

### JSON Web Token:
#### Register the `IJsonWebToken` service using by specifying the settings values
```csharp
builder.Services.AddGitViweJsonWebToken(options =>
{
    var key = Encoding.ASCII.GetBytes("vxL2V6EEj8HjgU6NxMhcNWAf0Ejxmcuj");

    options.Issuer = "https://localhost";
    options.RefreshValidationParameters = // create new instance with your settings;
    options.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
    options.TokenExpiry = TimeSpan.FromMinutes(int.Parse(5));
    options.ValidationParameters = // create new instance with your settings;
});
```

### Usage:

```csharp
IJsonWebToken {
    string CreateJsonWebToken(IEnumerable<Claim> claims, string audience);
    Task<ClaimsPrincipal?> ValidateToken(string token, bool isRefreshToken = false);
}
```