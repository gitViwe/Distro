﻿namespace gitViwe.Shared;

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

    /// <summary>
    /// Defines the telemetry source name for any external process
    /// </summary>
    public const string EXTERNAL_PROCESS = "External.Process";
}
