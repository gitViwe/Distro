namespace gitViwe.Shared.TimeBasedOneTimePassword;

/// <summary>
/// Facilitates generating and verifying time-based one-time password (TOTP).
/// </summary>
public interface ITimeBasedOneTimePassword
{
    /// <summary>
    /// Generates the time-based one-time password (TOTP) secret key and link.
    /// </summary>
    /// <param name="username">The user's display name.</param>
    /// <returns>A <see cref="TimeBasedOneTimePasswordLinkResponse"/> representing the result. Else, null if it fails.</returns>
    TimeBasedOneTimePasswordLinkResponse? GenerateLink(string username);

    /// <summary>
    /// Verify the time-based one-time password (TOTP).
    /// </summary>
    /// <param name="secretKey">The unique key used to verify the time-based one-time password (TOTP) token.</param>
    /// <param name="token">The current time-based one-time password (TOTP) from the user.</param>
    /// <returns>True if the time-based one-time password (TOTP) is valid. Else, false.</returns>
    bool VerifyToken(string secretKey, string token);
}

/// <summary>
/// Contains the time-based one-time password (TOTP) <paramref name="SecretKey"/> and <paramref name="Link"/>.
/// </summary>
/// <param name="SecretKey">The unique key used to verify the time-based one-time password (TOTP) token.</param>
/// <param name="Link">The otpauth:// URI used by Authenticator apps to generate one-time passcodes.</param>
public record TimeBasedOneTimePasswordLinkResponse(string SecretKey, string Link);