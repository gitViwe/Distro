namespace gitViwe.Shared.Extension;

/// <summary>
/// Extension methods for getting <seealso cref="JwtRegisteredClaimNames"/> values from <see cref="ClaimsPrincipal"/>
/// </summary>
public static class ClaimsPrincipalExtension
{
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
