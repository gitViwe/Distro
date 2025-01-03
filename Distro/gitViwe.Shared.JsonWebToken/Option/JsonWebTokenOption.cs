namespace gitViwe.Shared.JsonWebToken.Option;

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
    [Required]
    [DefaultValue(30)]
    [Range(30, int.MaxValue, ErrorMessage = "Please enter a value bigger than {29}")]
    public int ExpiryInSeconds { get; init; }

    /// <summary>
    /// The issuer of the <seealso cref="SecurityTokenDescriptor"/>.
    /// </summary>
    [Required]
    public string Issuer { get; init; } = string.Empty;
    
    /// <summary>
    /// The 32 character security key string.
    /// </summary>
    [Required]
    [MinLength(32)]
    public string Secret { get; init; } = string.Empty;

    /// <summary>
    /// The collection that contains valid issuers that will be used to check against the token's issuer. 
    /// </summary>
    [MinimumItems(1)]
    public IEnumerable<string> ValidIssuers { get; init; } = [];
    
    /// <summary>
    /// The collection that contains valid audiences that will be used to check against the token's audience. 
    /// </summary>
    [MinimumItems(1)]
    public IEnumerable<string> ValidAudiences { get; init; } = [];
    
    /// <summary>
    /// A boolean to control if the audience will be validated during token validation. 
    /// </summary>
    [DefaultValue(true)]
    public bool ValidateAudience { get; init; }
    
    /// <summary>
    /// A boolean to control if the issuer will be validated during token validation. 
    /// </summary>
    [DefaultValue(true)]
    public bool ValidateIssuer { get; init; }

    /// <summary>
    /// Gets the SigningCredentials used to create a security token.
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
