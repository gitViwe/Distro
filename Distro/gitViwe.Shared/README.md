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

### Utility:

Some helper classes
```csharp
static class Conversion {
    static long DateTimeToUnixTimeStamp(DateTime dateTime);
    static byte[] ParseBase64WithoutPadding(string payload);
    static string ByteArrayToHexadecimalString(byte[] value);
    static string ToObfuscatedString<TValue>(TValue request);
    static DateTime UnixTimeStampToDateTime(long unixTimeStamp);
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
        
        public static void StartActivity(
            string activityName,
            string eventName,
            ActivityStatusCode? statusCode = null,
            IEnumerable<KeyValuePair<string, object?>>? tags = null);
        
    }
    static class InternalProcess {
        
        public static void StartActivity(
            string activityName,
            string eventName,
            System.Exception exception);
        
        public static void StartActivity(
            string activityName,
            string eventName,
            ActivityStatusCode? statusCode = null,
            IEnumerable<KeyValuePair<string, object?>>? tags = null);
        
    }
    static class Instrumentation {
        
        static void EnrichWithHttpRequestHeaders(
            Activity activity,
            IEnumerable<KeyValuePair<string, StringValues>> headers);
        
        static void EnrichWithHttpRequestHeaders(
            Activity activity,
            IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers);
        
        static void EnrichWithHttpResponseHeaders(
            Activity activity,
            IEnumerable<KeyValuePair<string, StringValues>> headers);
        
        static void EnrichWithHttpResponseHeaders(
            Activity activity,
            IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers);
        
    }
}
```

### Wrapper:

A unified return type for the API endpoint.
```csharp
Response {
    static IResponse Fail(string message);
    static IResponse Fail(string message, int statusCode);
    static IResponse Success(string message);
    static IResponse Success(string message, int statusCode);
    static ITypedResponse<TData> Fail<TData>(string message);
    static ITypedResponse<TData> Fail<TData>(string message, int statusCode);
    static ITypedResponse<TData> Success<TData>(string message, TData data);
    static ITypedResponse<TData> Success<TData>(string message, int statusCode, TData data);
}

TypedResponse<TData> {
    static ITypedResponse<TData> Fail(string message);
    static ITypedResponse<TData> Fail(string message, int statusCode);
    static ITypedResponse<TData> Success(string message, TData data);
    static ITypedResponse<TData> Success(string message, int statusCode, TData data);
}

PaginatedResponse<TData> {
    static PaginatedResponse<TData> Success(IEnumerable<TData> data, int totalCount, int currentPage, int pageSize);
    static PaginatedResponse<TData> Success(IEnumerable<TData> dataToPaginate, int currentPage, int pageSize);
}
```