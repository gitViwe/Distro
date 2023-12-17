namespace gitViwe.Shared.MediatR.Option;

/// <summary>
/// Configuration options for the following behaviours:
/// <br /> <see cref="OpenTelemetryNotificationPreProcessor{TRequest}"/>
/// <br /> <see cref="OpenTelemetryRequestPreProcessor{TRequest, TResponse}"/>
/// </summary>
public class OpenTelemetryBehaviourOption
{
    /// <summary>
    /// The property names to be obfuscated
    /// </summary>
    public IEnumerable<string>? ObfuscatedPropertyNames { get; set; }
}
