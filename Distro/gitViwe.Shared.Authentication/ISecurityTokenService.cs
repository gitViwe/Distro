﻿using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

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
    /// <returns>A new <see cref="SecurityTokenDescriptor"/> instance</returns>
    SecurityTokenDescriptor CreateSecurityTokenDescriptor(IEnumerable<Claim> claims);

    /// <summary>
    /// Creates a JSON web token
    /// </summary>
    /// <param name="tokenDescriptor">The descriptor containing information which used to create a security token</param>
    /// <returns>A new <see cref="SecurityToken"/> instance</returns>
    SecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor);

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

    /// <summary>
    /// Generate the required claims from the claims principal
    /// </summary>
    /// <param name="claimsPrincipal">The claims principal to use</param>
    /// <returns>A collection of only the following claims if they are present: <br></br>
    /// <see cref="ClaimTypes.NameIdentifier"/> <br></br>
    /// <see cref="JwtRegisteredClaimNames.Jti"/>
    /// </returns>A collection of claims</returns>
    IEnumerable<Claim> GetRequiredClaims(ClaimsPrincipal claimsPrincipal);
}