using System.Text;
using gitViwe.Shared.JsonWebToken;

namespace gitViwe.Shared.JsonWebToken;

/// <summary>
/// Configuration options for creating a security token.
/// </summary>
public sealed class JsonWebTokenOption
{
    /// <summary>
    /// The configuration values from the "JsonWebTokenOption" section inside the appsettings.json file.
    /// </summary>
    public const string SectionName = "JsonWebTokenOption";

    /// <summary>
    /// The Security Algorithm to use.
    /// </summary>
    private const string _securityAlgorithm = SecurityAlgorithms.HmacSha256Signature;
    
    /// <summary>
    /// The value of the 'expiration' claim.
    /// </summary>
    public int ExpiryInSeconds { get; init; }

    /// <summary>
    /// The issuer of the <seealso cref="SecurityTokenDescriptor"/>.
    /// </summary>
    public required string Issuer { get; init; }
    
    /// <summary>
    /// The 32 character security key string.
    /// </summary>
    public required string Secret { get; init; }

    /// <summary>
    /// The collection that contains valid issuers that will be used to check against the token's issuer. 
    /// </summary>
    public IEnumerable<string> ValidIssuers { get; init; } = [];
    
    /// <summary>
    /// The collection that contains valid audiences that will be used to check against the token's audience. 
    /// </summary>
    public IEnumerable<string> ValidAudiences { get; init; } = [];
    
    /// <summary>
    /// A boolean to control if the audience will be validated during token validation. 
    /// </summary>
    public bool ValidateAudience { get; init; }
    
    /// <summary>
    /// A boolean to control if the issuer will be validated during token validation. 
    /// </summary>
    public bool ValidateIssuer { get; init; }

    /// <summary>
    /// Gets or sets the SigningCredentials used to create a security token.
    /// </summary>
    public SigningCredentials SigningCredentials =>
        new(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret)), _securityAlgorithm);

    /// <summary>
    /// Contains a set of parameters that are used by a <see cref="SecurityTokenHandler"/> when validating a <see cref="SecurityToken"/>
    /// </summary>
    public TokenValidationParameters ValidationParameters => CreateTokenValidationParameters(true);

    /// <summary>
    /// Contains a set of parameters that are used by a <see cref="SecurityTokenHandler"/> when validating a <see cref="SecurityToken"/>
    /// </summary>
    public TokenValidationParameters RefreshValidationParameters => CreateTokenValidationParameters(false);

    private TokenValidationParameters CreateTokenValidationParameters(bool validateLifetime) =>
        new()
        {
            ValidIssuers = ValidIssuers,
            ValidAudiences = ValidAudiences,
            ValidateIssuer = ValidateIssuer,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = validateLifetime,
            ValidateAudience = ValidateAudience,
            ValidAlgorithms = [ _securityAlgorithm ],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret)),
        };
}

internal class JsonWebTokenOptionValidator : IValidateOptions<JsonWebTokenOption>
{
    public ValidateOptionsResult Validate(string? name, JsonWebTokenOption options)
    {
        if (options.ExpiryInSeconds <= 0)
        {
            return ValidateOptionsResult.Fail($"{name}.{nameof(options.ExpiryInSeconds)} must be greater than 0.");
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