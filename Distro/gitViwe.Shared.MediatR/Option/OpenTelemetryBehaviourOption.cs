namespace gitViwe.Shared.MediatR.Option;

/// <summary>
/// Configuration options for <see cref="OpenTelemetryBehaviour{TRequest, TResponse}"/>
/// </summary>
public class OpenTelemetryBehaviourOption
{
    /// <summary>
    /// The property names to be obfuscated
    /// </summary>
    public string[]? ObfuscatedPropertyNames { get; set; }
}
