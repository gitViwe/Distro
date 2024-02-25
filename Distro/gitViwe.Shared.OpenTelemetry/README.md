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
        Activity? ExtractTraceContextFromHeaders(string activityName, string eventName, IEnumerable<KeyValuePair<string, object>> headers);
    }
 }
```