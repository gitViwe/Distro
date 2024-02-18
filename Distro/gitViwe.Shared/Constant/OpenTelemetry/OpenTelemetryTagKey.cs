namespace gitViwe.Shared.Constant.OpenTelemetry;

/// <summary>
/// Defines the custom telemetry tag keys
/// </summary>
public static class OpenTelemetryTagKey
{
    /// <summary>
    /// Defines the custom telemetry tag key prefix
    /// </summary>
    public const string PREFIX = "gitviwe.otlp.";

    /// <summary>
    /// Defines the custom telemetry tag keys for <seealso cref="OpenTelemetrySource.MEDIATR"/>
    /// </summary>
    public static class MediatR
    {
        /// <summary>
        /// Defines the custom telemetry tag key for the type
        /// </summary>
        public const string REQUEST_TYPE = PREFIX + "mediatr.request.type";

        /// <summary>
        /// Defines the custom telemetry tag key for the value
        /// </summary>
        public const string REQUEST_VALUE = PREFIX + "mediatr.request.value";

        /// <summary>
        /// Defines the custom telemetry tag key for the validator type
        /// </summary>
        public const string REQUEST_VALIDATOR = PREFIX + "mediatr.request.validator";

        /// <summary>
        /// Defines the custom telemetry tag key for the status code
        /// </summary>
        public const string RESPONSE_STATUS_CODE = PREFIX + "mediatr.response.status_code";

        /// <summary>
        /// Defines the custom telemetry tag key for the message
        /// </summary>
        public const string RESPONSE_MESSAGE = PREFIX + "mediatr.response.message";
    }

    /// <summary>
    /// Defines the custom telemetry tag keys for instrumenting HTTP servers / clients
    /// </summary>
    public static class HTTP
    {
        /// <summary>
        /// Defines the custom telemetry tag key for HTTP request
        /// </summary>
        public const string REQUEST_HEADER = PREFIX + "http.request.header";

        /// <summary>
        /// Defines the custom telemetry tag key for HTTP response
        /// </summary>
        public const string RESPONSE_HEADER = PREFIX + "http.response.header";
    }

    /// <summary>
    /// Defines the custom telemetry tag keys for common JWT values
    /// </summary>
    public static class JWT
    {
        /// <summary>
        /// Defines the custom telemetry tag keys for the user id
        /// </summary>
        public const string USER_ID = PREFIX + "jwt.user_id";

        /// <summary>
        /// Defines the custom telemetry tag keys for jwt id
        /// </summary>
        public const string JWT_ID = PREFIX + "jwt.id";

        /// <summary>
        /// Defines the custom telemetry tag keys for jwt issuer
        /// </summary>
        public const string JWT_ISSUER = PREFIX + "jwt.issuer";

        /// <summary>
        /// Defines the custom telemetry tag keys for jwt audience
        /// </summary>
        public const string JWT_AUDIENCE = PREFIX + "jwt.audience";
    }

    /// <summary>
    /// Defines the custom telemetry tag keys for endpoint filters
    /// </summary>
    public static class EndpointFilter
    {
        /// <summary>
        /// Defines the custom telemetry tag keys for endpoint filter type
        /// </summary>
        public const string FILTER_TYPE = PREFIX + "endpoint_filter.type";

    }
}
