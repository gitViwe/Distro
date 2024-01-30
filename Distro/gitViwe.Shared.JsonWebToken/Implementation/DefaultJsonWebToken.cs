namespace gitViwe.Shared.JsonWebToken;

internal class DefaultJsonWebToken(IOptions<JsonWebTokenOption> options, ILogger<DefaultJsonWebToken> logger) : IJsonWebToken
{
    private readonly JsonWebTokenOption _options = options.Value;
    private readonly ILogger<DefaultJsonWebToken> _logger = logger;

    public string CreateJsonWebToken(IEnumerable<Claim> claims, string audience)
    {
        SecurityTokenDescriptor descriptor = new()
        {
            Audience = audience,
            Issuer = _options.Issuer,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(_options.TokenExpiry),
            SigningCredentials = _options.SigningCredentials,
        };

        return new JsonWebTokenHandler().CreateToken(descriptor);
    }

    public async Task<ClaimsPrincipal?> ValidateToken(string token, bool isRefreshToken = false)
    {
        JsonWebTokenHandler handler = new();

        TokenValidationResult result = isRefreshToken
                ? await handler.ValidateTokenAsync(token, _options.RefreshValidationParameters)
                : await handler.ValidateTokenAsync(token, _options.ValidationParameters);

        if (false == result.IsValid)
        {
            _logger.FailedToValidateJsonWebToken(result.Exception, result.TokenOnFailedValidation);
            return null;
        }

        if (result.SecurityToken is Microsoft.IdentityModel.JsonWebTokens.JsonWebToken securityToken)
        {
            // verify that the token is encrypted with the security algorithm
            if (false == securityToken.Alg.Equals(_options.SigningCredentials!.Algorithm, StringComparison.InvariantCultureIgnoreCase))
            {
                _logger.InvalidJsonWebTokenAlgorithm(result.SecurityToken, securityToken);
                return null;
            }
        }

        return new ClaimsPrincipal(result.ClaimsIdentity);
    }
}
