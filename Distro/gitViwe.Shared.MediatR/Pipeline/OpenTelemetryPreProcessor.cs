namespace gitViwe.Shared.MediatR;

/// <summary>
/// Creates a trace pipeline for recording MediatR request details
/// </summary>
/// <typeparam name="TRequest">The MediatR type</typeparam>
public class OpenTelemetryPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly OpenTelemetryBehaviourOption _options;

    /// <summary>
    /// Creates a new instance of <see cref="OpenTelemetryPreProcessor{TRequest}"/>
    /// </summary>
    /// <param name="options">The option values</param>
    public OpenTelemetryPreProcessor(IOptions<OpenTelemetryBehaviourOption> options)
    {
        _options = options.Value;
    }

    /// <summary>
    /// Records the MediatR type and values
    /// </summary>
    /// <param name="request">The MediatR request</param>
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
            { OpenTelemetryTagKey.MediatR.REQUEST_TYPE, request.GetType().Name },
            { OpenTelemetryTagKey.MediatR.REQUEST_VALUE, ToObfuscatedString() },
        };

        OpenTelemetryActivity.MediatR.StartActivity("OpenTelemetry MediatR PreProcessor", "Starting MediatR Request.", requestTagDictionary);

        return Task.CompletedTask;
    }
}
