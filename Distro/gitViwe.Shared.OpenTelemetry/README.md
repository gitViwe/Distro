## gitViwe.Shared.OpenTelemetry

### Nuget package:
```
dotnet add package gitViwe.Shared.OpenTelemetry 
```

Some custom utility methods
```csharp
class OpenTelemetryPropagator {
    class ExternalProcess {
        void InjectTraceContextToHeaders(string activityName, string eventName, IDictionary<string, object> headers, ActivityKind kind = ActivityKind.Server);

        Activity? ExtractTraceContextFromHeaders(
            string activityName,
            string eventName,
            IEnumerable<KeyValuePair<string, string>> headers,
            ActivityKind kind = ActivityKind.Client,
            IEnumerable<KeyValuePair<string, object?>>? tags = null);

        IDisposable? ExtractTraceContext(string activityName, string eventName, string parentId);
    }
 }
```