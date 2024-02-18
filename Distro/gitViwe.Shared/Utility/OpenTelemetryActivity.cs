using System.Diagnostics;
using gitViwe.Shared.Constant.OpenTelemetry;
using Microsoft.Extensions.Primitives;

namespace gitViwe.Shared.Utility;

/// <summary>
/// Exposes some <see cref="ActivitySource"/> methods for the various <seealso cref="OpenTelemetrySource"/>
/// </summary>
public static class OpenTelemetryActivity
{
    /// <summary>
    /// Defines some <see cref="ActivitySource"/> methods for <seealso cref="OpenTelemetrySource.MEDIATR"/>
    /// </summary>
    public static class MediatR
    {
        private static readonly ActivitySource _activitySource = new(OpenTelemetrySource.MEDIATR);

        /// <summary>
        /// Starts a new <see cref="Activity"/> if there is any listener to the Activity source <seealso cref="OpenTelemetrySource.MEDIATR"/>
        /// </summary>
        /// <param name="activityName">The operation name of the Activity</param>
        /// <param name="statusCode">The status of the current activity</param>
        /// <param name="eventName">The event name</param>
        public static void StartActivity(string activityName, string eventName, ActivityStatusCode statusCode = ActivityStatusCode.Unset)
        {
            using var activity = _activitySource.StartActivity(activityName);
            activity?.SetStatus(statusCode);
            activity?.AddEvent(new ActivityEvent(eventName));
        }

        /// <summary>
        /// Starts a new <see cref="Activity"/> if there is any listener to the Activity source <seealso cref="OpenTelemetrySource.MEDIATR"/>
        /// </summary>
        /// <param name="activityName">The operation name of the Activity</param>
        /// <param name="eventName">The event name</param>
        /// <param name="tags">The event tags</param>
        /// <param name="statusCode">The status of the current activity</param>
        public static void StartActivity(string activityName, string eventName, Dictionary<string, object?> tags, ActivityStatusCode statusCode = ActivityStatusCode.Unset)
        {
            using var activity = _activitySource.StartActivity(activityName);
            activity?.SetStatus(statusCode);
            activity?.AddEvent(new ActivityEvent(eventName, tags: new ActivityTagsCollection(tags)));
        }
    }

    /// <summary>
    /// Defines some <see cref="ActivitySource"/> methods for <seealso cref="OpenTelemetrySource.INTERNAL_PROCESS"/>
    /// </summary>
    public static class InternalProcess
    {
        private static readonly ActivitySource _activitySource = new(OpenTelemetrySource.INTERNAL_PROCESS);

        /// <summary>
        /// Starts a new <see cref="Activity"/> if there is any listener to the Activity source <seealso cref="OpenTelemetrySource.INTERNAL_PROCESS"/>
        /// </summary>
        /// <param name="activityName">The operation name of the Activity</param>
        /// <param name="statusCode">The status of the current activity</param>
        /// <param name="eventName">The event name</param>
        public static void StartActivity(string activityName, string eventName, ActivityStatusCode statusCode = ActivityStatusCode.Unset)
        {
            using var activity = _activitySource.StartActivity(activityName);
            activity?.SetStatus(statusCode);
            activity?.AddEvent(new ActivityEvent(eventName));
        }

        /// <summary>
        /// Starts a new <see cref="Activity"/> if there is any listener to the Activity source <seealso cref="OpenTelemetrySource.INTERNAL_PROCESS"/>
        /// </summary>
        /// <param name="activityName">The operation name of the Activity</param>
        /// <param name="eventName">The event name</param>
        /// <param name="tags">The event tags</param>
        /// <param name="statusCode">The status of the current activity</param>
        public static void StartActivity(string activityName, string eventName, Dictionary<string, object?> tags, ActivityStatusCode statusCode = ActivityStatusCode.Unset)
        {
            using var activity = _activitySource.StartActivity(activityName);
            activity?.SetStatus(statusCode);
            activity?.AddEvent(new ActivityEvent(eventName, tags: new ActivityTagsCollection(tags)));
        }

        /// <summary>
        /// Starts a new <see cref="Activity"/> if there is any listener to the Activity source <seealso cref="OpenTelemetrySource.INTERNAL_PROCESS"/>
        /// </summary>
        /// <param name="activityName">The operation name of the Activity</param>
        /// <param name="eventName">The event name</param>
        /// <param name="exception">The exception to record</param>
        public static void StartActivity(string activityName, string eventName, System.Exception exception)
        {
            Dictionary<string, object?> tagDictionary = new()
            {
                { "exception.message", exception.Message },
                { "exception.stacktrace", exception.StackTrace },
                { "exception.type", exception.GetType().FullName },
            };

            using var activity = _activitySource.StartActivity(activityName);
            activity?.SetStatus(ActivityStatusCode.Error);
            activity?.AddEvent(new ActivityEvent(eventName, tags: new ActivityTagsCollection(tagDictionary)));
        }
    }

    /// <summary>
    /// Defines some enricher methods for instrumented frameworks
    /// </summary>
    public static class Instrumentation
    {
        /// <summary>
        /// The default request paths to ignore.
        /// </summary>
        public readonly static IEnumerable<string> DefaultFilterRequestPath =
        [
            "/",
            "/_vs/browserLink",
            "/_framework/aspnetcore-browser-refresh.js",
            "/favicon.ico",
            "/swagger/index.html",
            "/swagger/favicon-32x32.png",
            "/swagger/v1/swagger.json",
            "/swagger/swagger-ui-bundle.js",
            "/swagger/swagger-ui-bundle.js",
            "/swagger/swagger-ui.css",
        ];

        /// <summary>
        /// Enrich the <paramref name="activity"/> by recording HTTP request headers
        /// </summary>
        /// <param name="activity">Represents an operation with context to be used for logging.</param>
        /// <param name="headers">The HTTP request headers</param>
        public static void EnrichWithHttpRequestHeaders(Activity activity, IEnumerable<KeyValuePair<string, StringValues>> headers)
        {
            activity?.AddEvent(new ActivityEvent("HTTP Request Headers", tags: GetTagsFromHeaders(headers, OpenTelemetryTagKey.HTTP.REQUEST_HEADER)));
        }

        /// <summary>
        /// Enrich the <paramref name="activity"/> by recording HTTP response headers
        /// </summary>
        /// <param name="activity">Represents an operation with context to be used for logging.</param>
        /// <param name="headers">The HTTP response headers</param>
        public static void EnrichWithHttpResponseHeaders(Activity activity, IEnumerable<KeyValuePair<string, StringValues>> headers)
        {
            activity?.AddEvent(new ActivityEvent("HTTP Response Headers", tags: GetTagsFromHeaders(headers, OpenTelemetryTagKey.HTTP.RESPONSE_HEADER)));
        }

        /// <summary>
        /// Enrich the <paramref name="activity"/> by recording HTTP request headers
        /// </summary>
        /// <param name="activity">Represents an operation with context to be used for logging.</param>
        /// <param name="headers">The HTTP request headers</param>
        public static void EnrichWithHttpRequestHeaders(Activity activity, IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers)
        {
            activity?.AddEvent(new ActivityEvent("HTTP Request Headers", tags: GetTagsFromHeaders(headers, OpenTelemetryTagKey.HTTP.REQUEST_HEADER)));
        }

        /// <summary>
        /// Enrich the <paramref name="activity"/> by recording HTTP response headers
        /// </summary>
        /// <param name="activity">Represents an operation with context to be used for logging.</param>
        /// <param name="headers">The HTTP response headers</param>
        public static void EnrichWithHttpResponseHeaders(Activity activity, IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers)
        {
            activity?.AddEvent(new ActivityEvent("HTTP Response Headers", tags: GetTagsFromHeaders(headers, OpenTelemetryTagKey.HTTP.RESPONSE_HEADER)));
        }

        private static ActivityTagsCollection? GetTagsFromHeaders(IEnumerable<KeyValuePair<string, StringValues>> headers, string tagKeyPrefix)
        {
            if (false == headers.Any()) { return null; }

            Dictionary<string, object?> tags = [];

            foreach (var header in headers)
            {
                tags.Add(key: $"{tagKeyPrefix}.{header.Key.ToLower()}", string.Join(';', header.Value!));
            }

            return tags.Count != 0 ? new ActivityTagsCollection(tags) : null;
        }

        private static ActivityTagsCollection? GetTagsFromHeaders(IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers, string tagKeyPrefix)
        {
            if (false == headers.Any()) { return null; }

            Dictionary<string, object?> tags = [];

            foreach (var header in headers)
            {
                tags.Add(key: $"{tagKeyPrefix}.{header.Key.ToLower()}", string.Join(';', header.Value!));
            }

            return tags.Count != 0 ? new ActivityTagsCollection(tags) : null;
        }
    }
}
