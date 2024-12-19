using System.Diagnostics;
using gitViwe.Shared.Constant.OpenTelemetry;
using Microsoft.Extensions.Primitives;

namespace gitViwe.Shared.Utility;

/// <summary>
/// Exposes some <see cref="ActivitySource"/> methods for the various <seealso cref="OpenTelemetrySource"/>
/// </summary>
public static class OpenTelemetryActivity
{
    private static void StartActivity(
        ActivitySource activitySource,
        string activityName,
        string eventName,
        ActivityStatusCode? statusCode = null,
        IEnumerable<KeyValuePair<string, object?>>? tags = null)
    {
        using var activity = activitySource.StartActivity(activityName);
            
        if (statusCode.HasValue)
        {
            activity?.SetStatus(statusCode.Value);
        }

        if (tags is not null)
        {
            activity?.AddEvent(new ActivityEvent(eventName, tags: new ActivityTagsCollection(tags)));
        }
    }
    
    private static void StartActivity(
        ActivitySource activitySource,
        string activityName,
        string eventName,
        System.Exception exception)
    {
        Dictionary<string, object?> tagDictionary = new()
        {
            { "exception.message", exception.Message },
            { "exception.stacktrace", exception.StackTrace },
            { "exception.type", exception.GetType().FullName },
        };

        using var activity = activitySource.StartActivity(activityName);
        activity?.SetStatus(ActivityStatusCode.Error);
        activity?.AddEvent(new ActivityEvent(eventName, tags: new ActivityTagsCollection(tagDictionary)));
    }
    
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
        /// <param name="eventName">The event name</param>
        /// <param name="tags">The event tags</param>
        /// <param name="statusCode">The status of the current activity</param>
        public static void StartActivity(
            string activityName,
            string eventName,
            ActivityStatusCode? statusCode = null,
            IEnumerable<KeyValuePair<string, object?>>? tags = null)
            => OpenTelemetryActivity.StartActivity(_activitySource, activityName, eventName, statusCode, tags);
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
        /// <param name="eventName">The event name</param>
        /// <param name="tags">The event tags</param>
        /// <param name="statusCode">The status of the current activity</param>
        public static void StartActivity(
            string activityName,
            string eventName,
            ActivityStatusCode? statusCode = null,
            IEnumerable<KeyValuePair<string, object?>>? tags = null)
            => OpenTelemetryActivity.StartActivity(_activitySource, activityName, eventName, statusCode, tags);

        /// <summary>
        /// Starts a new <see cref="Activity"/> if there is any listener to the Activity source <seealso cref="OpenTelemetrySource.INTERNAL_PROCESS"/>
        /// </summary>
        /// <param name="activityName">The operation name of the Activity</param>
        /// <param name="eventName">The event name</param>
        /// <param name="exception">The exception to record</param>
        public static void StartActivity(
            string activityName,
            string eventName,
            System.Exception exception)
            => OpenTelemetryActivity.StartActivity(_activitySource, activityName, eventName, exception);
    }

    /// <summary>
    /// Defines some enricher methods for instrumented frameworks
    /// </summary>
    public static class Instrumentation
    {
        /// <summary>
        /// Enrich the <paramref name="activity"/> by recording HTTP request headers
        /// </summary>
        /// <param name="activity">Represents an operation with context to be used for logging.</param>
        /// <param name="headers">The HTTP request headers</param>
        public static void EnrichWithHttpRequestHeaders(Activity activity, IEnumerable<KeyValuePair<string, StringValues>> headers)
        {
            activity.AddEvent(new ActivityEvent("HTTP Request Headers", tags: GetTagsFromHeaders(headers, OpenTelemetryTagKey.HTTP.REQUEST_HEADER)));
        }

        /// <summary>
        /// Enrich the <paramref name="activity"/> by recording HTTP response headers
        /// </summary>
        /// <param name="activity">Represents an operation with context to be used for logging.</param>
        /// <param name="headers">The HTTP response headers</param>
        public static void EnrichWithHttpResponseHeaders(Activity activity, IEnumerable<KeyValuePair<string, StringValues>> headers)
        {
            activity.AddEvent(new ActivityEvent("HTTP Response Headers", tags: GetTagsFromHeaders(headers, OpenTelemetryTagKey.HTTP.RESPONSE_HEADER)));
        }

        /// <summary>
        /// Enrich the <paramref name="activity"/> by recording HTTP request headers
        /// </summary>
        /// <param name="activity">Represents an operation with context to be used for logging.</param>
        /// <param name="headers">The HTTP request headers</param>
        public static void EnrichWithHttpRequestHeaders(Activity activity, IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers)
        {
            activity.AddEvent(new ActivityEvent("HTTP Request Headers", tags: GetTagsFromHeaders(headers, OpenTelemetryTagKey.HTTP.REQUEST_HEADER)));
        }

        /// <summary>
        /// Enrich the <paramref name="activity"/> by recording HTTP response headers
        /// </summary>
        /// <param name="activity">Represents an operation with context to be used for logging.</param>
        /// <param name="headers">The HTTP response headers</param>
        public static void EnrichWithHttpResponseHeaders(Activity activity, IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers)
        {
            activity.AddEvent(new ActivityEvent("HTTP Response Headers", tags: GetTagsFromHeaders(headers, OpenTelemetryTagKey.HTTP.RESPONSE_HEADER)));
        }

        private static ActivityTagsCollection? GetTagsFromHeaders(IEnumerable<KeyValuePair<string, StringValues>> headers, string tagKeyPrefix)
        {
            var tags = headers
                .Select(header => new KeyValuePair<string, object?>($"{tagKeyPrefix}.{header.Key.ToLower()}", string.Join(';', header.Value!)))
                .ToArray();

            return tags.Length == 0 ? null : new ActivityTagsCollection(tags);
        }

        private static ActivityTagsCollection? GetTagsFromHeaders(IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers, string tagKeyPrefix)
        {
            var tags = headers
                .Select(header => new KeyValuePair<string, object?>($"{tagKeyPrefix}.{header.Key.ToLower()}", string.Join(';', header.Value)))
                .ToArray();

            return tags.Length == 0 ? null : new ActivityTagsCollection(tags);
        }
    }
}
