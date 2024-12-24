namespace gitViwe.Shared.JsonWebToken;

internal sealed class DefaultJsonWebToken(IOptionsMonitor<JsonWebTokenOption> options, ILogger<DefaultJsonWebToken> logger) : IJsonWebToken
{
    private readonly JsonWebTokenOption _options = options.CurrentValue;

    public string CreateJsonWebToken(IEnumerable<Claim> claims, string audience)
    {
        SecurityTokenDescriptor descriptor = new()
        {
            Audience = audience,
            Issuer = _options.Issuer,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddSeconds(_options.ExpiryInSeconds),
            SigningCredentials = _options.SigningCredentials,
        };

        return new JsonWebTokenHandler().CreateToken(descriptor);
    }

    public async Task<ClaimsPrincipal?> ValidateTokenAsync(string token, bool isRefreshToken = false)
    {
        JsonWebTokenHandler handler = new();

        TokenValidationResult result = isRefreshToken
                ? await handler.ValidateTokenAsync(token, _options.RefreshValidationParameters)
                : await handler.ValidateTokenAsync(token, _options.ValidationParameters);

        if (false == result.IsValid)
        {
            logger.FailedToValidateJsonWebToken(result.Exception, result.TokenOnFailedValidation);
            return null;
        }

        if (result.SecurityToken is Microsoft.IdentityModel.JsonWebTokens.JsonWebToken securityToken)
        {
            // verify that the token is encrypted with the security algorithm
            if (false == securityToken.Alg.Equals(_options.SigningCredentials.Algorithm, StringComparison.InvariantCultureIgnoreCase))
            {
                logger.InvalidJsonWebTokenAlgorithm(result.SecurityToken, securityToken);
                return null;
            }
        }

        return new ClaimsPrincipal(result.ClaimsIdentity);
    }
}
