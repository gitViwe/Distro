namespace gitViwe.Shared.OpenTelemetry.MediatR;

/// <summary>
/// Creates a trace pipeline for recording MediatR request details
/// </summary>
/// <typeparam name="TRequest">The MediatR type</typeparam>
/// <remarks>
/// Add the <seealso cref="OpenTelemetrySource.MEDIATR"/> source to register a listener for these traces
/// </remarks>
/// <param name="options">The option values</param>
public sealed class OpenTelemetryPreProcessor<TRequest>(IOptions<OpenTelemetryBehaviourOption> options) : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly OpenTelemetryBehaviourOption _options = options.Value;

    /// <summary>
    /// Records the MediatR type and values
    /// </summary>
    /// <param name="request">The MediatR request</param>
    /// <param name="cancellationToken">The Cancellation token to notify that operations should be cancelled</param>
    /// <returns>A <see cref="Task"/> that represents the process</returns>
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        Dictionary<string, object?> requestTagDictionary = new()
        {
            { OpenTelemetryTagKey.MediatR.REQUEST_TYPE, request.GetType().Name },
            { OpenTelemetryTagKey.MediatR.REQUEST_VALUE, Conversion.ToObfuscatedString(request, _options.ObfuscatedPropertyNames) },
        };

        OpenTelemetryActivity.MediatR.StartActivity("OpenTelemetry MediatR PreProcessor", "Starting MediatR Request.", tags: requestTagDictionary);

        return Task.CompletedTask;
    }
}
