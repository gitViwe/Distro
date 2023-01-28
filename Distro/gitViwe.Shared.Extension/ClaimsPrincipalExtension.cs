using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace gitViwe.Shared.Extension;

/// <summary>
/// Extension methods for getting values from <see cref="ClaimsPrincipal"/>
/// </summary>
public static class ClaimsPrincipalExtension
{
    /// <summary>
    /// Gets the Email value from the claims
    /// </summary>
    /// <param name="claimsPrincipal">The current <see cref="ClaimsPrincipal"/></param>
    /// <returns>The <seealso cref="ClaimTypes.Email"/> value</returns>
    public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
    }

    /// <summary>
    /// Gets the JWT Id
    /// </summary>
    /// <param name="claimsPrincipal">The current <see cref="ClaimsPrincipal"/></param>
    /// <returns>The <seealso cref="JwtRegisteredClaimNames.Jti"/> value</returns>
    public static string GetTokenID(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value ?? string.Empty;
    }

    /// <summary>
    /// Gets the NameIdentifier value from the claims
    /// </summary>
    /// <param name="claimsPrincipal">The current <see cref="ClaimsPrincipal"/></param>
    /// <returns>The <seealso cref="ClaimTypes.NameIdentifier"/> value</returns>
    public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
    }

    /// <summary>
    /// Gets the Sub value from the claims
    /// </summary>
    /// <param name="claimsPrincipal">The current <see cref="ClaimsPrincipal"/></param>
    /// <returns>The <seealso cref="ClaimTypes.Name"/> value</returns>
    public static string GetUsername(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
    }

    /// <summary>
    /// Checks if the JWT claims have or are close to expiring
    /// </summary>
    /// <param name="claimsPrincipal">The current <see cref="ClaimsPrincipal"/></param>
    /// <param name="thresholdInMinutes">The time until the expiry time of the token</param>
    /// <returns>True if the <seealso cref="JwtRegisteredClaimNames.Exp"/> value will be reached in <paramref name="thresholdInMinutes"/></returns>
    public static bool HasExpiredClaims(this ClaimsPrincipal claimsPrincipal, int thresholdInMinutes = 5)
    {
        // verify that the expiry date has not passed
        if (long.TryParse(claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Exp)?.Value, out long expiry))
        {
            var expiryDate = Conversion.UnixTimeStampToDateTime(expiry);
            if (expiryDate > DateTime.UtcNow.AddMinutes(-thresholdInMinutes))
            {
                // token has not yet expired
                return false;
            }
        }
        return true;
    }
}