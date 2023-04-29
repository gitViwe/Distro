using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace gitViwe.Shared.Authentication.Option;

/// <summary>
/// Configuration options for creating a security token.
/// </summary>
public class SecurityTokenOption
{
    /// <summary>
    /// Gets or sets the value of the 'audience' claim.
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value of the 'expiration' claim.
    /// </summary>
    public TimeSpan TokenExpiry { get; set; }

    /// <summary>
    /// Gets or sets the issuer of the <seealso cref="SecurityTokenDescriptor"/>.
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the SigningCredentials used to create a security token.
    /// </summary>
    public SigningCredentials SigningCredentials { get; set; }

    /// <summary>
    /// Contains a set of parameters that are used by a <see cref="SecurityTokenHandler"/> when validating a <see cref="SecurityToken"/>
    /// </summary>
    public TokenValidationParameters ValidationParameters { get; set; }

    /// <summary>
    /// Contains a set of parameters that are used by a <see cref="SecurityTokenHandler"/> when validating a <see cref="SecurityToken"/>
    /// </summary>
    public TokenValidationParameters RefreshValidationParameters { get; set; }
}

internal class SecurityTokenOptionValidator : IValidateOptions<SecurityTokenOption>
{
    public ValidateOptionsResult Validate(string? name, SecurityTokenOption options)
    {
        if (string.IsNullOrWhiteSpace(options.Audience))
        {
            return ValidateOptionsResult.Fail($"A value for {name}.{nameof(options.Audience)} must be provided.");
        }

        if (options.TokenExpiry.CompareTo(TimeSpan.Zero) < 1)
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