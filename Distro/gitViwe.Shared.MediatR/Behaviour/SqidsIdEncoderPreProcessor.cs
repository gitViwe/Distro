namespace gitViwe.Shared.MediatR.Behaviour;

/// <summary>
/// Creates a pipeline for decoding a Sqids Id MediatR request details
/// </summary>
/// <typeparam name="TRequest">The MediatR type</typeparam>
/// <remarks>
/// Ensure the call to <seealso cref="Startup.AddGitViweSqidsIdEncoder(IServiceCollection)"/> is made.
/// </remarks>
public class SqidsIdEncoderPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly ISqidsIdEncoder<int> _encoder;
    private readonly ILogger<SqidsIdEncoderPreProcessor<TRequest>> _logger;

    /// <summary>
    /// Creates a new instance of <see cref="SqidsIdEncoderPreProcessor{TRequest}"/>
    /// </summary>
    public SqidsIdEncoderPreProcessor(
        ISqidsIdEncoder<int> encoder,
        ILogger<SqidsIdEncoderPreProcessor<TRequest>> logger)
    {
        _encoder = encoder;
        _logger = logger;
    }

    /// <summary>
    /// Decodes the encoded Sqids id into the properties decorated with <see cref="DecodedSqidsIdAttribute"/>
    /// </summary>
    /// <param name="request">The MediatR request</param>
    /// <param name="cancellationToken">The Cancellation token to notify that operations should be cancelled</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        OpenTelemetryActivity.MediatR.StartActivity("SqidsIdEncoder MediatR PreProcessor", "Starting MediatR Request.");

        var allProperties = typeof(TRequest).GetProperties();
        if (allProperties is null) { return Task.CompletedTask; }

        var targetProperties = allProperties.Where(prop => prop.GetCustomAttribute<DecodedSqidsIdAttribute>() is not null);
        if (targetProperties is null) { return Task.CompletedTask; }

        foreach (var target in targetProperties)
        {
            var attribute = target.GetCustomAttribute<DecodedSqidsIdAttribute>();

            if (false == string.IsNullOrWhiteSpace(attribute!.EncodedSqidsId)
                && target.PropertyType == typeof(int)
                && target.CanWrite)
            {
                string pattern = "[^a-zA-Z0-9]";
                string alphaNumeric = Regex.Replace(attribute!.EncodedSqidsId, pattern, string.Empty);

                if (_encoder.TryDecode(alphaNumeric, out int decoded))
                {
                    target.SetValue(request, decoded);
                    _logger.LogInformation("Decoded Sqids id. {encoded} {decoded} {target.property}", alphaNumeric, decoded, target.Name);
                }
            }
        }

        return Task.CompletedTask;
    }
}
