using gitViwe.Shared.JsonWebToken.Option;

namespace gitViwe.Shared.JsonWebToken.Implementation;

internal sealed class DefaultJsonWebToken(
    IOptions<JsonWebTokenOption> options,
    ILogger<DefaultJsonWebToken> logger) : IJsonWebToken
{
    private readonly JsonWebTokenOption _options = options.Value;

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

        return new ClaimsPrincipal(result.ClaimsIdentity);
    }
}
