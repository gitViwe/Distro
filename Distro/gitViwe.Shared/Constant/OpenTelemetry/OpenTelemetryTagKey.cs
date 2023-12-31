namespace gitViwe.Shared.Constant.OpenTelemetry;

/// <summary>
/// Defines the custom telemetry tag keys
/// </summary>
public static class OpenTelemetryTagKey
{
    private const string _prefix = "gitviwe.otlp.";

    /// <summary>
    /// Defines the custom telemetry tag keys for <seealso cref="OpenTelemetrySource.MEDIATR"/>
    /// </summary>
    public static class MediatR
    {
        /// <summary>
        /// Defines the custom telemetry tag key for the type
        /// </summary>
        public const string REQUEST_TYPE = _prefix + "mediatr.request.type";

        /// <summary>
        /// Defines the custom telemetry tag key for the value
        /// </summary>
        public const string REQUEST_VALUE = _prefix + "mediatr.request.value";

        /// <summary>
        /// Defines the custom telemetry tag key for the validator type
        /// </summary>
        public const string REQUEST_VALIDATOR = _prefix + "mediatr.request.validator";

        /// <summary>
        /// Defines the custom telemetry tag key for the status code
        /// </summary>
        public const string RESPONSE_STATUS_CODE = _prefix + "mediatr.response.status_code";

        /// <summary>
        /// Defines the custom telemetry tag key for the message
        /// </summary>
        public const string RESPONSE_MESSAGE = _prefix + "mediatr.response.message";
    }

    /// <summary>
    /// Defines the custom telemetry tag keys for instrumenting HTTP servers / clients
    /// </summary>
    public static class HTTP
    {
        /// <summary>
        /// Defines the custom telemetry tag key for HTTP request
        /// </summary>
        public const string REQUEST_HEADER = _prefix + "http.request.header";

        /// <summary>
        /// Defines the custom telemetry tag key for HTTP response
        /// </summary>
        public const string RESPONSE_HEADER = _prefix + "http.response.header";
    }

    /// <summary>
    /// Defines the custom telemetry tag keys for common JWT values
    /// </summary>
    public static class JWT
    {
        /// <summary>
        /// Defines the custom telemetry tag keys for the user id
        /// </summary>
        public const string USER_ID = _prefix + "jwt.user_id";

        /// <summary>
        /// Defines the custom telemetry tag keys for jwt id
        /// </summary>
        public const string JWT_ID = _prefix + "jwt.id";

        /// <summary>
        /// Defines the custom telemetry tag keys for jwt issuer
        /// </summary>
        public const string JWT_ISSUER = _prefix + "jwt.issuer";

        /// <summary>
        /// Defines the custom telemetry tag keys for jwt audience
        /// </summary>
        public const string JWT_AUDIENCE = _prefix + "jwt.audience";
    }
}
