using System.Diagnostics;
using gitViwe.Shared.Constant.OpenTelemetry;

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
        /// <param name="eventName">The event name</param>
        public static void StartActivity(string activityName, string eventName)
        {
            using var activity = _activitySource.StartActivity(activityName);
            activity?.AddEvent(new ActivityEvent(eventName));
        }

        /// <summary>
        /// Starts a new <see cref="Activity"/> if there is any listener to the Activity source <seealso cref="OpenTelemetrySource.MEDIATR"/>
        /// </summary>
        /// <param name="activityName">The operation name of the Activity</param>
        /// <param name="eventName">The event name</param>
        /// <param name="tags">The event tags</param>
        public static void StartActivity(string activityName, string eventName, Dictionary<string, object?> tags)
        {
            using var activity = _activitySource.StartActivity(activityName);
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
        /// <param name="eventName">The event name</param>
        public static void StartActivity(string activityName, string eventName)
        {
            using var activity = _activitySource.StartActivity(activityName);
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
}
