namespace gitViwe.Shared.OpenTelemetry.MediatR;

/// <summary>
/// Creates a trace pipeline for recording MediatR response details
/// </summary>
/// <typeparam name="TRequest">The MediatR request type</typeparam>
/// <typeparam name="TResponse">The MediatR response type</typeparam>
public class OpenTelemetryPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : IResponse
{
    /// <summary>
    /// Records the response type and values
    /// </summary>
    /// <param name="request">The MediatR request command or query</param>
    /// <param name="response">The MediatR response</param>
    /// <param name="cancellationToken">The Cancellation token to notify that operations should be cancelled</param>
    /// <returns>A <see cref="Task"/> that represents the process</returns>
    public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        Dictionary<string, object?> responseTagDictionary = new()
        {
            { OpenTelemetryTagKey.MediatR.RESPONSE_STATUS_CODE, response.StatusCode },
            { OpenTelemetryTagKey.MediatR.RESPONSE_MESSAGE, response.Message },
        };

        OpenTelemetryActivity.MediatR.StartActivity("OpenTelemetry MediatR PostProcessor", "Completing MediatR Request.", responseTagDictionary);

        return Task.CompletedTask;
    }
}
