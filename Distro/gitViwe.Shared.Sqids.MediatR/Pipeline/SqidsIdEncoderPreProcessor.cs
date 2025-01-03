﻿namespace gitViwe.Shared.Sqids.MediatR;

/// <summary>
/// Creates a pipeline for decoding a Sqids Id for MediatR requests that implement <see cref="ISqidsIdEncoderPreProcessMarker"/>
/// </summary>
/// <typeparam name="TRequest">The MediatR type</typeparam>
/// <remarks>
/// Ensure the call to <seealso cref="Startup.AddGitViweSqidsIdEncoder(IServiceCollection)"/> is made. <br/>
/// Add the <seealso cref="OpenTelemetrySource.MEDIATR"/> source to register a listener for these traces.
/// </remarks>
/// <remarks>
/// Creates a new instance of <see cref="SqidsIdEncoderPreProcessor{TRequest}"/>
/// </remarks>
public sealed class SqidsIdEncoderPreProcessor<TRequest>(
    ISqidsIdEncoder<int> encoder,
    ILogger<SqidsIdEncoderPreProcessor<TRequest>> logger) : IRequestPreProcessor<TRequest>
    where TRequest : ISqidsIdEncoderPreProcessMarker
{
    /// <summary>
    /// Decodes the encoded Sqids id into the properties decorated with <see cref="DecodedSqidsIdAttribute"/>
    /// </summary>
    /// <param name="request">The MediatR request</param>
    /// <param name="cancellationToken">The Cancellation token to notify that operations should be cancelled</param>
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        OpenTelemetryActivity.MediatR.StartActivity("Sqids Id Encoder PreProcessor", "Starting Sqids Id Decode.");

        var allProperties = typeof(TRequest).GetProperties();

        var targetProperties = allProperties.Where(prop => prop.GetCustomAttribute<DecodedSqidsIdAttribute>() is not null);

        foreach (var target in targetProperties)
        {
            var attribute = target.GetCustomAttribute<DecodedSqidsIdAttribute>();
            var source = allProperties.FirstOrDefault(prop => prop.Name.Equals(attribute!.SourceProperty));

            if (source is not null
                && source.PropertyType == typeof(string)
                && target.PropertyType == typeof(int)
                && target.CanWrite)
            {
                var encoded = source.GetValue(request) as string;
                if (string.IsNullOrWhiteSpace(encoded)) { continue; }
                
                string alphaNumeric = Regex.Replace(encoded, "[^a-zA-Z0-9]", string.Empty);

                if (encoder.TryDecode(alphaNumeric, out int decoded))
                {
                    target.SetValue(request, decoded);
                    logger.SqidsIdDecoded(alphaNumeric, decoded, target.Name);
                }
            }
        }

        return Task.CompletedTask;
    }
}
