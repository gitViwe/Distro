namespace gitViwe.Shared.Constant.OpenTelemetry;

/// <summary>
/// Defines the telemetry source names
/// </summary>
public static class OpenTelemetrySource
{
    /// <summary>
    /// Defines the telemetry source name for MediatR
    /// </summary>
    public const string MEDIATR = "MediatR";

    /// <summary>
    /// Defines the telemetry source name for MongoDB
    /// </summary>
    public const string MONGODB = "MongoDB.Driver.Core.Extensions.DiagnosticSources";

    /// <summary>
    /// Defines the telemetry source name for any internal process
    /// </summary>
    public const string INTERNAL_PROCESS = "Internal.Process";
}
