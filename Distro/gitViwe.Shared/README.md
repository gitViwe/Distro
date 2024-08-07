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
static class HubEnvironmentName { }
static class OpenTelemetrySource { }
static class OpenTelemetryTagKey {
    static class MediatR { }
    static class HTTP { }
    static class JWT { }
    static class EndpointFilter { }
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
        static void StartActivity(string activityName, string eventName, ActivityStatusCode statusCode = ActivityStatusCode.Unset);
        static void StartActivity(string activityName, string eventName, IEnumerable<KeyValuePair<string, object?>> tags, ActivityStatusCode statusCode = ActivityStatusCode.Unset);
    }
    static class InternalProcess {
        static void StartActivity(string activityName, string eventName, ActivityStatusCode statusCode = ActivityStatusCode.Unset);
        static void StartActivity(string activityName, string eventName, System.Exception exception);
        static void StartActivity(string activityName, string eventName, IEnumerable<KeyValuePair<string, object?>> tags, ActivityStatusCode statusCode = ActivityStatusCode.Unset);
    }
}

static class Instrumentation {
    static IEnumerable<string> DefaultFilterRequestPath;
    static void EnrichWithHttpRequestHeaders(Activity activity, IEnumerable<KeyValuePair<string, StringValues>> headers);
    static void EnrichWithHttpRequestHeaders(Activity activity, IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers);
    static void EnrichWithHttpResponseHeaders(Activity activity, IEnumerable<KeyValuePair<string, StringValues>> headers);
    static void EnrichWithHttpResponseHeaders(Activity activity, IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers);
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
    static PaginatedResponse<TData> Success(IEnumerable<TData> data, int totalCount, int currentPage, int pageSize);
    static PaginatedResponse<TData> Success(IEnumerable<TData> dataToPaginate, int currentPage, int pageSize);
}
```