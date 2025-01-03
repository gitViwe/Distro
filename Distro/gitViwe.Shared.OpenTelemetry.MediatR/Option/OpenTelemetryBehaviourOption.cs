namespace gitViwe.Shared.OpenTelemetry.MediatR;

/// <summary>
/// Configuration options for the following behaviours:
/// <br /> <see cref="OpenTelemetryPreProcessor{TRequest}"/>
/// </summary>
public sealed class OpenTelemetryBehaviourOption
{
    /// <summary>
    /// The configuration values from the "OpenTelemetryBehaviourOption" section inside the appsettings.json file.
    /// </summary>
    public const string SectionName = "OpenTelemetryBehaviourOption";
    
    /// <summary>
    /// The property names to be obfuscated
    /// </summary>
    public IEnumerable<string> ObfuscatedPropertyNames { get; init; } = [];
}
