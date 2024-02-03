namespace gitViwe.Shared.JsonWebToken;

/// <summary>
/// Configuration options for creating a security token.
/// </summary>
public class JsonWebTokenOption
{
    /// <summary>
    /// Gets or sets the value of the 'expiration' claim.
    /// </summary>
    public TimeSpan TokenExpiry { get; set; }

    /// <summary>
    /// Gets or sets the issuer of the <seealso cref="SecurityTokenDescriptor"/>.
    /// </summary>
    public required string Issuer { get; set; }

    /// <summary>
    /// Gets or sets the SigningCredentials used to create a security token.
    /// </summary>
    public required SigningCredentials SigningCredentials { get; set; }

    /// <summary>
    /// Contains a set of parameters that are used by a <see cref="SecurityTokenHandler"/> when validating a <see cref="SecurityToken"/>
    /// </summary>
    public required TokenValidationParameters ValidationParameters { get; set; }

    /// <summary>
    /// Contains a set of parameters that are used by a <see cref="SecurityTokenHandler"/> when validating a <see cref="SecurityToken"/>
    /// </summary>
    public required TokenValidationParameters RefreshValidationParameters { get; set; }
}

internal class JsonWebTokenOptionValidator : IValidateOptions<JsonWebTokenOption>
{
    public ValidateOptionsResult Validate(string? name, JsonWebTokenOption options)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            name = "JsonWebTokenOption";
        }

        if (options.TokenExpiry.CompareTo(TimeSpan.Zero) <= 0)
        {
            return ValidateOptionsResult.Fail($"{name}.{nameof(options.TokenExpiry)} must be a time span in the future.");
        }

        if (string.IsNullOrWhiteSpace(options.Issuer))
        {
            return ValidateOptionsResult.Fail($"A value for {name}.{nameof(options.Issuer)} must be provided.");
        }

        if (options.SigningCredentials is null)
        {
            return ValidateOptionsResult.Fail($"{name}.{nameof(options.SigningCredentials)} must be provided.");
        }

        if (options.ValidationParameters is null)
        {
            return ValidateOptionsResult.Fail($"{name}.{nameof(options.ValidationParameters)} must be provided.");
        }

        if (options.RefreshValidationParameters is null)
        {
            return ValidateOptionsResult.Fail($"{name}.{nameof(options.RefreshValidationParameters)} must be provided.");
        }

        return ValidateOptionsResult.Success;
    }
}