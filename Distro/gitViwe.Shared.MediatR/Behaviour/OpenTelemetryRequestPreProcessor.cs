namespace gitViwe.Shared.MediatR.Behaviour;

/// <summary>
/// Creates a trace pipeline for recording MediatR request details
/// </summary>
/// <typeparam name="TRequest">The MediatR request type</typeparam>
/// <typeparam name="TResponse">The MediatR response type</typeparam>
public class OpenTelemetryRequestPreProcessor<TRequest, TResponse> : IRequestPreProcessor<TRequest>
    where TRequest : IRequest<TResponse>
{
    private readonly OpenTelemetryBehaviourOption _options;

    /// <summary>
    /// Creates a new instance of <see cref="OpenTelemetryRequestPreProcessor{TRequest, TResponse}"/>
    /// </summary>
    /// <param name="options">The option values</param>
    public OpenTelemetryRequestPreProcessor(IOptions<OpenTelemetryBehaviourOption> options)
    {
        _options = options.Value;
    }

    /// <summary>
    /// Records the request type and values
    /// </summary>
    /// <param name="request">The MediatR request command or query</param>
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

        OpenTelemetryActivity.MediatR.StartActivity("OpenTelemetry Request PreProcessor", "Starting MediatR Request.", requestTagDictionary);

        return Task.CompletedTask;
    }
}
