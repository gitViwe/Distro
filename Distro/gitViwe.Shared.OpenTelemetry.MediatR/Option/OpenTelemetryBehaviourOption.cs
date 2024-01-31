namespace gitViwe.Shared.OpenTelemetry.MediatR;

/// <summary>
/// Configuration options for the following behaviours:
/// <br /> <see cref="OpenTelemetryPreProcessor{TRequest}"/>
/// </summary>
public class OpenTelemetryBehaviourOption
{
    /// <summary>
    /// The property names to be obfuscated
    /// </summary>
    public IEnumerable<string> ObfuscatedPropertyNames { get; set; } = Enumerable.Empty<string>();
}
