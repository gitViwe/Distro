namespace gitViwe.Shared.JsonWebToken;

/// <summary>
/// Facilitates generating and verifying JSON Web Tokens.
/// </summary>
public interface IJsonWebToken
{
    /// <summary>
    /// Creates a JSON web token string.
    /// </summary>
    /// <param name="claims">The claims to add to the security token.</param>
    /// <param name="audience">The recipient for which the JWT is intended.</param>
    /// <returns>A string representing the JSON web token.</returns>
    string CreateJsonWebToken(IEnumerable<Claim> claims, string audience);

    /// <summary>
    /// Reads and validates a 'JSON Web Token' (JWT).
    /// </summary>
    /// <param name="token">The JSON web token to validate.</param>
    /// <param name="isRefreshToken">If set to true, the token expiry will not be validated.</param>
    /// <returns>The <see cref="Task{TResult}"/> containing the <see cref="ClaimsPrincipal"/>.</returns>
    Task<ClaimsPrincipal?> ValidateTokenAsync(string token, bool isRefreshToken = false);
}
