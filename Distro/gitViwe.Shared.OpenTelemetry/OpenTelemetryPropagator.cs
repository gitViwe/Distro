﻿namespace gitViwe.Shared.OpenTelemetry;

/// <summary>
/// Exposes some <see cref="ActivitySource"/> methods for the various <seealso cref="OpenTelemetrySource"/>
/// </summary>
public static class OpenTelemetryPropagator
{
    /// <summary>
    /// Defines some <see cref="ActivitySource"/> methods for <seealso cref="OpenTelemetrySource.EXTERNAL_PROCESS"/>
    /// </summary>
    public static class ExternalProcess
    {
        private static readonly ActivitySource _activitySource = new(OpenTelemetrySource.EXTERNAL_PROCESS);
        private static readonly TextMapPropagator _propagator = Propagators.DefaultTextMapPropagator;

        /// <summary>
        /// Starts a new <see cref="Activity"/> if there is any listener to the Activity source <seealso cref="OpenTelemetrySource.EXTERNAL_PROCESS"/>
        /// </summary>
        /// <remarks>
        /// Injects the ActivityContext into the <paramref name="headers"/> to propagate trace context to the receiving service.
        /// </remarks>
        /// <param name="activityName">The operation name of the Activity</param>
        /// <param name="eventName">The event name</param>
        /// <param name="headers">The dictionary where the trace context will be injected.</param>
        public static void InjectTraceContextToHeaders(string activityName, string eventName, IDictionary<string, object> headers)
        {
            using var activity = _activitySource.StartActivity(activityName, ActivityKind.Producer);
            activity?.AddEvent(new ActivityEvent(eventName));

            ActivityContext contextToInject = activity?.Context ?? Activity.Current?.Context ?? default;

            _propagator.Inject(new PropagationContext(contextToInject, Baggage.Current), headers, InjectTraceContext);

            static void InjectTraceContext(IDictionary<string, object> headers, string key, string value)
            {
                headers[key] = value;
            }
        }

        /// <summary>
        /// Starts a new <see cref="Activity"/> if there is any listener to the Activity source <seealso cref="OpenTelemetrySource.EXTERNAL_PROCESS"/>
        /// </summary>
        /// <remarks>
        /// Extracts the PropagationContext of the upstream parent from the <paramref name="headers"/>.
        /// </remarks>
        /// <param name="activityName">The operation name of the Activity</param>
        /// <param name="eventName">The event name</param>
        /// <param name="headers">The dictionary where the trace context will be extracted.</param>
        public static void ExtractTraceContextFromHeaders(string activityName, string eventName, IEnumerable<KeyValuePair<string, object>> headers)
        {
            static IEnumerable<string> ExtractTraceContext(IEnumerable<KeyValuePair<string, object>> headers, string key)
            {
                try
                {
                    return headers.Select(x => x.Value.ToString()!);
                }
                catch (System.Exception) { }

                return Enumerable.Empty<string>();
            }

            var parentContext = _propagator.Extract(default, headers, ExtractTraceContext);

            // Need to filter the contents of baggage we want to inject
            // Baggage.Current = parentContext.Baggage;

            using var activity = _activitySource.StartActivity(activityName, ActivityKind.Producer);
            activity?.AddEvent(new ActivityEvent(eventName));
        }
    }
}