namespace gitViwe.Shared.JsonWebToken.Extension;

internal static partial class DefaultJsonWebTokenLoggerExtension
{
    /// <summary>
    /// Failed to validate the JSON Web Token.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="exception">The exception thrown.</param>
    /// <param name="securityToken">The security token to be returned when validation fails.</param>
    [LoggerMessage(
        Level = LogLevel.Warning,
        Message = "Failed to validate the JSON Web Token: {SecurityToken}")]
    public static partial void FailedToValidateJsonWebToken(this ILogger logger,
        Exception exception,
        [LogProperties] SecurityToken securityToken);

    /// <summary>
    /// Failed to validate the JSON Web Token.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="securityToken">The security token to be returned when validation passes.</param>
    /// <param name="jsonWebToken">The JsonWebToken derived from the base class <paramref name="securityToken"/>.</param>
    [LoggerMessage(
        Level = LogLevel.Warning,
        Message = "Invalid JSON web token algorithm: {SecurityToken} {JsonWebToken}")]
    public static partial void FailedToValidateJsonWebTokenAlgorithm(this ILogger logger,
        [LogProperties] SecurityToken securityToken,
        [LogProperties] Microsoft.IdentityModel.JsonWebTokens.JsonWebToken jsonWebToken);
}
