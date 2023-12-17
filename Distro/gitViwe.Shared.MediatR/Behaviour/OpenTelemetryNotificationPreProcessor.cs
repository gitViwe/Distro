namespace gitViwe.Shared.MediatR.Behaviour;

/// <summary>
/// Creates a trace pipeline for recording MediatR notification details
/// </summary>
/// <typeparam name="TRequest">The MediatR notification type</typeparam>
public class OpenTelemetryNotificationPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    where TRequest : INotification
{
    private readonly OpenTelemetryBehaviourOption _options;

    /// <summary>
    /// Creates a new instance of <see cref="OpenTelemetryNotificationPreProcessor{TRequest}"/>
    /// </summary>
    /// <param name="options">The option values</param>
    public OpenTelemetryNotificationPreProcessor(IOptions<OpenTelemetryBehaviourOption> options)
    {
        _options = options.Value;
    }

    /// <summary>
    /// Records the notification type and values
    /// </summary>
    /// <param name="request">The MediatR notification</param>
    /// <param name="cancellationToken">The Cancellation token to notify that operations should be cancelled</param>
    /// <returns>A <see cref="Task"/> that represents the process</returns>
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        string ToObfuscatedString()
        {
            return _options.ObfuscatedPropertyNames is not null
                ? Conversion.ToObfuscatedString(request, _options.ObfuscatedPropertyNames)
                : Conversion.ToObfuscatedString(request);
        }

        Dictionary<string, object?> requestTagDictionary = new()
        {
            { OpenTelemetryTagKey.MediatR.NOTIFICATION_TYPE, request.GetType().Name },
            { OpenTelemetryTagKey.MediatR.NOTIFICATION_VALUE, ToObfuscatedString() },
        };

        OpenTelemetryActivity.MediatR.StartActivity("OpenTelemetry Notification PreProcessor", "Starting MediatR Notification.", requestTagDictionary);

        return Task.CompletedTask;
    }
}
