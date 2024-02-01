# Distro
This repository will house a collection of shared libraries that will be distributed through NuGet that should help quickly launch projects I would like to play around with.


## gitViwe.Shared

```
dotnet add package gitViwe.Shared 
```

### Abstraction:

Defines abstractions for the custom classes
```csharp
interface IPaginatedRequest { }
interface IRequiresHost { }
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
class BaseException { }
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
    static string ToObfuscatedString<TValue>(TValue request, IEnumerable<string> propertyNames);
}

static class Formatter {
    static string FormatSize(long bytes)
}

static class Generator {
    static string RandomString(CharacterCombination combination, int length)
}

static class OpenTelemetryActivity {
    static class MediatR {
        static void StartActivity(string activityName, string eventName);
        static void StartActivity(string activityName, string eventName, Dictionary<string, object?> tags, ActivityStatusCode statusCode = ActivityStatusCode.Unset);
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

## gitViwe.Shared.Cache

### Nuget package:
```
dotnet add package gitViwe.Shared.Cache 
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
interface IRedisDistributedCache {
    TResult? Get<TResult>(string key);
    string? Get(string key);
    Task<TResult?> GetAsync<TResult>(string key, CancellationToken token = default);
    Task<string?> GetAsync(string key, CancellationToken token = default);
    void Set<TValue>(string key, TValue value, TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null);
    Task SetAsync<TValue>(string key, TValue value, TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null, CancellationToken token = default);
    void Refresh(string key);
    Task RefreshAsync(string key, CancellationToken token = default);
    void Remove(string key);
    Task RemoveAsync(string key, CancellationToken token = default);
}
```

## gitViwe.Shared.Extension

### Nuget package:
```
dotnet add package gitViwe.Shared.Extension 
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
dotnet add package gitViwe.Shared.FluentValidation 
```

### Extensions:

Some custom FluentValidation methods
```csharp
class FluentValidatorExtension {
    static IRuleBuilderOptions<T, string> MustBeValidUri<T>(this IRuleBuilder<T, string> ruleBuilder);
    static IRuleBuilderOptions<T, string> NotContain<T>(this IRuleBuilder<T, string> ruleBuilder, string searchString);
}
```

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


## gitViwe.Shared.Imgbb

### Nuget package:
```
dotnet add package gitViwe.Shared.Imgbb 
```

### Image hosting service using Imgbb:
#### Register the `IImgBBClient` service by specifying the settings values
```
builder.Services.AddGitViweImgBBClient(options =>
{
    options.APIKey = "my-secret-key";
    options.ExpirationInSeconds = 180;
});
```

### Usage:

```csharp
IImgBBClient {
    Task<ImgBBUploadResponse> UploadImageAsync(IFormFile file, int? expirationInSeconds = null, CancellationToken cancellation = default);
    Task<ImgBBUploadResponse> UploadImageAsync(HttpContent httpContent, string fileName, int? expirationInSeconds = null, CancellationToken cancellation = default);
}
```

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

## gitViwe.Shared.MongoDB

### Nuget package:
```
dotnet add package gitViwe.Shared.MongoDB 
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

## gitViwe.Shared.OpenTelemetry

### Nuget package:
```
dotnet add package gitViwe.Shared.OpenTelemetry 
```

Some custom utility methods
```csharp
class OpenTelemetryPropagator {
    class ExternalProcess {
        void InjectTraceContextToHeaders(string activityName, string eventName, IDictionary<string, object> headers);
        void ExtractTraceContextFromHeaders(string activityName, string eventName, IEnumerable<KeyValuePair<string, object>> headers);
    }
 }
```

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

## Problem Details

### Nuget package:
```
dotnet add package gitViwe.Shared.ProblemDetail 
```

### Usage:

```csharp
static class ProblemDetailFactory {
    static IDefaultProblemDetails CreateProblemDetails(int statusCode, string instance, string? detail = null);
    static IDefaultProblemDetails CreateProblemDetails(int statusCode, string instance, IDictionary<string, object?> extensions, string? detail = null);
    static IValidationProblemDetails CreateValidationProblemDetails(int statusCode, string instance, IDictionary<string, string[]> errors, string? detail = null);
}
```

## gitViwe.Shared.ProblemDetail.Extension

### Nuget package:
```
dotnet add package gitViwe.Shared.ProblemDetail.Extension 
```

Some helpful extension methods
```csharp
class ResponseExtension {
    static async Task<IDefaultProblemDetails> ToProblemResponseAsync(
        this HttpResponseMessage response,
        JsonSerializerOptions? options = null,
        CancellationToken token = default);
    static async Task<IValidationProblemDetails> ToValidationProblemResponseAsync(
        this HttpResponseMessage response,
        JsonSerializerOptions? options = null,
        CancellationToken token = default);
}
```

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
