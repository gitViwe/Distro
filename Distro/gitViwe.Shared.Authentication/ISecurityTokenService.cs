using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace gitViwe.Shared.Authentication;

/// <summary>
/// Helper service to facilitate JSON Web Token token processing
/// </summary>
public interface ISecurityTokenService
{
    /// <summary>
    /// Creates a descriptor containing information which used to create a security token
    /// </summary>
    /// <param name="claims">The claims to add to the security token</param>
    /// <param name="audience">The recipient for which the JWT is intended</param>
    /// <returns>A new <see cref="SecurityTokenDescriptor"/> instance</returns>
    SecurityTokenDescriptor CreateSecurityTokenDescriptor(IEnumerable<Claim> claims, string? audience = null);

    /// <summary>
    /// Creates a JSON web token
    /// </summary>
    /// <param name="tokenDescriptor">The descriptor containing information which used to create a security token</param>
    /// <returns>A new <see cref="SecurityToken"/> instance</returns>
    SecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor);

    /// <summary>
    /// Creates a JSON web token
    /// </summary>
    /// <param name="claims">The claims to add to the security token</param>
    /// <param name="audience">The recipient for which the JWT is intended</param>
    /// <returns>A new <see cref="SecurityToken"/> instance</returns>
    SecurityToken CreateToken(IEnumerable<Claim> claims, string? audience = null);

    /// <summary>
    /// Creates a JSON web token string
    /// </summary>
    /// <param name="token">The security token to use</param>
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="ArgumentException" />
    /// <exception cref="SecurityTokenEncryptionFailedException" />
    /// <returns>A string representing the JSON web token</returns>
    string WriteToken(SecurityToken token);

    /// <summary>
    /// Reads and validates a 'JSON Web Token' (JWT)
    /// </summary>
    /// <param name="token">The JSON web token to validate.</param>
    /// <param name="isRefreshToken">If set to true, the token expiry will not be validated</param>
    /// <exception cref="SecurityTokenValidationException" />
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="ArgumentException" />
    /// <returns>The <see cref="ClaimsPrincipal"/> from the JWT.</returns>
    ClaimsPrincipal ValidateToken(string token, bool isRefreshToken = false);
}
