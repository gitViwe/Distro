using gitViwe.Shared.Authentication.Option;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace gitViwe.Shared.Authentication;

internal class SecurityTokenService : ISecurityTokenService
{
    private readonly SecurityTokenOption _option;
    public SecurityTokenService(IOptionsMonitor<SecurityTokenOption> options)
    {
        _option = options.CurrentValue;
    }

    public SecurityTokenDescriptor CreateSecurityTokenDescriptor(IEnumerable<Claim> claims)
    {
        return new SecurityTokenDescriptor()
        {
            Audience = _option.Audience,
            Issuer = _option.Issuer,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(_option.TokenExpiry),
            SigningCredentials = _option.SigningCredentials,
        };
    }

    public SecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor)
    {
        return new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
    }

    public SecurityToken CreateToken(IEnumerable<Claim> claims)
    {
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Audience = _option.Audience,
            Issuer = _option.Issuer,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(_option.TokenExpiry),
            SigningCredentials = _option.SigningCredentials,
        };

        return new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
    }

    public ClaimsPrincipal ValidateToken(string token, bool isRefreshToken = false)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwtClaims = isRefreshToken
                ? handler.ValidateToken(token, _option.RefreshValidationParameters, out SecurityToken validatedToken)
                : handler.ValidateToken(token, _option.ValidationParameters, out validatedToken);

        ArgumentNullException.ThrowIfNull(jwtClaims, nameof(jwtClaims));

        if (validatedToken is JwtSecurityToken securityToken)
        {
            // verify that the token is encrypted with the security algorithm
            var result = securityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

            if (result is false) throw new ArgumentException("Invalid JSON web token.");
        }

        return jwtClaims;
    }

    public string WriteToken(SecurityToken token)
    {
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
