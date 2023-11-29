# Distro
This repository will house a collection of shared libraries that will be distributed through NuGet that should help quickly launch projects I would like to play around with.


## gitViwe.Shared

```
dotnet add package gitViwe.Shared --version 2.0.3
```

### Abstraction:

Defines the schema for the custom ProblemDetails class
```csharp
interface IDefaultProblemDetails { }
interface IPaginatedRequest { }
interface IRequiresHost {}
interface IValidationProblemDetails { }
```

### Attribute:

Some custom attributes
```csharp
class ObfuscateAttribute { }
```

### Constant:

Some helper classes
```csharp
static class OpenTelemetrySource { }
static class OpenTelemetryTagKey {
    static class MediatR { }
    static class HTTP { }
    static class JWT { }
}
```

### Exception:

Some custom exceptions
```csharp
class ForbiddenException { }
class NotFoundException { }
class UnauthorizedException { }
class ServiceUnavailableException { }
class ValidationException { }
```

### Utility:

Some helper classes
```csharp
static class Conversion {
    static string ByteArrayToString(byte[] value);
    static byte[] StringToByteArray(string hex);
    static string RandomString(int length);
    static DateTime UnixTimeStampToDateTime(long unixTimeStamp);
    static long DateTimeToUnixTimeStamp(DateTime dateTime);
    static byte[] ParseBase64WithoutPadding(string payload);
    static string ToObfuscatedString<TValue>(TValue request, params string[] propertyNames);
}

static class Formatter {
    static string FormatSize(long bytes)
}

static class Generator {
    static string RandomString(CharacterCombination combination, int length)
}

static class OpenTelemetryActivity {
    static class MediatR {
        static void StartActivity(string activityName, string eventName, Dictionary<string, object?> tags);
    }
    static class InternalProcess {
        static void StartActivity(string activityName, string eventName);
        static void StartActivity(string activityName, string eventName, System.Exception exception);
        static void StartActivity(string activityName, string eventName, Dictionary<string, object?> tags, ActivityStatusCode statusCode = ActivityStatusCode.Unset);
    }
}
```

### Wrapper:

A unified return type for the API endpoint.
```csharp
Response {
    static Response Fail(string message);
    static Response Fail(string message, int statusCode);
    static Response Success(string message);
    static Response Success(string message, int statusCode);
}

Response<TData> {
    static Response<TData> Fail(string message);
    static Response<TData> Fail(string message, int statusCode);
    static Response<TData> Success(string message, TData data);
    static Response<TData> Success(string message, int statusCode, TData data);
}

PaginatedResponse<TData> {
    static PaginatedResponse<TData> Success(IEnumerable<TData> data, int count, int page, int pageSize);
    static PaginatedResponse<TData> Success(IEnumerable<TData> dataToPaginate, int page, int pageSize);
}
```

## gitViwe.Shared.Authentication

### Nuget package:
```
dotnet add package gitViwe.Shared.Authentication --version 2.0.3
```

### JSON Web Token:
#### Register the `ISecurityTokenService` service using by specifying the settings values
```csharp
builder.Services.AddGitViweSecurityTokenService(options =>
{
    var key = Encoding.ASCII.GetBytes("vxL2V6EEj8HjgU6NxMhcNWAf0Ejxmcuj");

    options.Audience = "https://localhost";
    options.Issuer = "https://localhost";
    options.RefreshValidationParameters = // create new instance with your settings;
    options.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
    options.TokenExpiry = TimeSpan.FromMinutes(int.Parse(5));
    options.ValidationParameters = // create new instance with your settings;
});
```

### Usage:

```csharp
ISecurityTokenService {
    SecurityTokenDescriptor CreateSecurityTokenDescriptor(IEnumerable<Claim> claims, string? audience = null);
    SecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor);
    SecurityToken CreateToken(IEnumerable<Claim> claims, string? audience = null);
    string WriteToken(SecurityToken token);
    ClaimsPrincipal ValidateToken(string token, bool isRefreshToken = false);
}
```

### Time-based one-time password (TOTP):
#### Register the `ITimeBasedOTPService` service
```csharp
builder.Services.AddGitViweTimeBasedOTPService();
```

### Usage:

```csharp
ITimeBasedOTPService {
    QRCodeData GenerateQrCodeData(string username, string issuer, out string secretKey);
    byte[] GetGraphicAsByteArray(QRCodeData data, int pixelsPerModule = 20);
    bool VerifyTOTP(string secretKey, string token);
}
```


## gitViwe.Shared.Cache

### Nuget package:
```
dotnet add package gitViwe.Shared.Cache --version 2.0.3
```

### Redis distributed cache:
#### Register the `IRedisDistributedCache` service using by specifying the settings values
```
builder.Services.AddGitViweRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName= "redis_demo";
    options.AbsoluteExpirationInMinutes = 5;
    options.SlidingExpirationInMinutes = 2;
});
```

### Usage:

```csharp
public IActionResult Result([FromServices] IRedisDistributedCache redis, [FromBody] UrlShortenRequest request)
{
    await redis.SetAsync(key: "mykey", value: request.Uri, absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(request.MinutesUntilExpiry));

    string value = await redis.GetAsync(key: "mykey") ?? string.Empty;

    return value;
}
```

## gitViwe.Shared.Extension

### Nuget package:
```
dotnet add package gitViwe.Shared.Extension --version 2.0.3
```

Some helpful extension methods
```csharp
class ClaimsPrincipalExtension {
    static bool HasExpiredClaims(this ClaimsPrincipal claimsPrincipal, int thresholdInMinutes = 5);
}
class ResponseExtension {
    static async Task<TData> ToResponseAsync<TData>(
        this HttpResponseMessage response,
        JsonSerializerOptions? options = null,
        CancellationToken token = default);
    static async Task<PaginatedResponse<TData>> ToPaginatedResponseAsync<TData>(
        this HttpResponseMessage response,
        JsonSerializerOptions? options = null,
        CancellationToken token = default);
}
```

## gitViwe.Shared.FluentValidation

### Nuget package:
```
dotnet add package gitViwe.Shared.FluentValidation --version 2.0.3
```

### Extensions:

Some custom attributes
```csharp
class FluentValidatorExtension {
    static IRuleBuilderOptions<T, string> MustBeValidUri<T>(this IRuleBuilder<T, string> ruleBuilder);
    static IRuleBuilderOptions<T, string> NotContain<T>(this IRuleBuilder<T, string> ruleBuilder, string searchString);
}
```

## gitViwe.Shared.MediatR

### Nuget package:
```
dotnet add package gitViwe.Shared.MediatR --version 2.0.3
```

### Behaviour:

Some custom attributes
```csharp
class OpenTelemetryBehaviour<TRequest, TResponse> { }
class ValidationBehaviour<TRequest, TResponse> { }
```

#### Optional configuration for the `OpenTelemetryBehaviourOption` to be used by `OpenTelemetryBehaviour`
```csharp
builder.Services.ConfigureGitViweOpenTelemetryBehaviourOption(options =>
{
    options.ObfuscatedPropertyNames = new string[] { "Email", "Password", "PasswordConfirmation", "Token" };
});
```

## gitViwe.Shared.MongoDB

### Nuget package:
```
dotnet add package gitViwe.Shared.MongoDB --version 2.0.3
```

### Mongo document database:
#### Register the `IMongoDBRepository` service using by specifying the settings values
```csharp
builder.Services.AddGitViweMongoDBRepository(options =>
{
    options.MongoClientSettings = MongoClientSettings.FromConnectionString("mongodb://root:example@localhost:27017");
    options.DatabaseName = "hub-db";
});
```

### Usage:

```csharp
IMongoDBRepository<TMongoDocument> {
    IQueryable<TMongoDocument> AsQueryable();
    Task<long> DeleteOneAsync(Expression<Func<TMongoDocument, bool>> filterExpression, CancellationToken cancellationToken);
    Task<TMongoDocument> FindByIdAsync(string id, CancellationToken cancellationToken);
    Task<TMongoDocument> FirstOrDefaultAsync(Expression<Func<TMongoDocument, bool>> filterExpression, CancellationToken cancellationToken);
    Task ReplaceOneAsync(TMongoDocument document, CancellationToken cancellationToken);
    IEnumerable<TMongoDocument> ToEnumerable(Expression<Func<TMongoDocument, bool>> filterExpression, CancellationToken cancellationToken);
    Task<List<TMongoDocument>> ToListAsync(Expression<Func<TMongoDocument, bool>> filterExpression, CancellationToken cancellationToken);
}
```

## Problem Details

### Nuget package:
```
dotnet add package gitViwe.Shared.ProblemDetail --version 2.0.3
```

### Usage:

```csharp
var extensionValue = new
{
    Balance = 30.0m,
    Accounts = { "/account/12345", "/account/67890" }
};

var problem = ProblemDetailFactory.CreateProblemDetails(
                context: HttpContext,
                statusCode: StatusCodes.Status412PreconditionFailed,
                extensions: new Dictionary<string, object?>()
                {
                    { "outOfCredit", extensionValue }
                },
                detail: "Your current balance is 30, but that costs 50.");
```
