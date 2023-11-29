using Microsoft.Extensions.Options;

namespace gitViwe.Shared.MediatR.Behaviour;

/// <summary>
/// Creates a trace pipeline for recording MediatR request and response details
/// </summary>
/// <typeparam name="TRequest">The MediatR request type</typeparam>
/// <typeparam name="TResponse">The MediatR response type</typeparam>
public class OpenTelemetryBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResponse
{
    private readonly OpenTelemetryBehaviourOption _options;

    /// <summary>
    /// Creates a new instance of <see cref="OpenTelemetryBehaviour{TRequest, TResponse}"/>
    /// </summary>
    /// <param name="options">The option values</param>
    public OpenTelemetryBehaviour(IOptions<OpenTelemetryBehaviourOption> options)
    {
        _options = options.Value;
    }

    /// <summary>
    /// Records the request type and values 
    /// </summary>
    /// <param name="request">The MediatR request command or query</param>
    /// <param name="next">The delegate to handle the next function in the pipeline</param>
    /// <param name="cancellationToken">The Cancellation token to notify that operations should be canceled</param>
    /// <returns>The next MediatR function in the pipeline</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Dictionary<string, object?> requestTagDictionary = new()
        {
            { OpenTelemetryTagKey.MediatR.REQUEST_TYPE, request.GetType().Name },
            { OpenTelemetryTagKey.MediatR.REQUEST_VALUE, _options.ObfuscatedPropertyNames is null ? Conversion.ToObfuscatedString(request) : Conversion.ToObfuscatedString(request, _options.ObfuscatedPropertyNames) },
        };

        OpenTelemetryActivity.MediatR.StartActivity("OpenTelemetryBehaviour", "Starting MediatR Request.", requestTagDictionary);

        var response = await next();

        Dictionary<string, object?> responseTagDictionary = new()
        {
            { OpenTelemetryTagKey.MediatR.RESPONSE_STATUS_CODE, response.StatusCode },
            { OpenTelemetryTagKey.MediatR.RESPONSE_MESSAGE, response.Message },
        };

        OpenTelemetryActivity.MediatR.StartActivity("OpenTelemetryBehaviour", "Completing MediatR Request.", responseTagDictionary);

        return response;
    }
}
