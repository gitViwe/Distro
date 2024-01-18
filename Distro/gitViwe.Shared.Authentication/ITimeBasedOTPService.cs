using QRCoder;

namespace gitViwe.Shared.Authentication;

/// <summary>
/// Helper service for time-based one-time password (TOTP)
/// </summary>
public interface ITimeBasedOTPService
{
    /// <summary>
    /// Creates a QR code image.
    /// </summary>
    /// <param name="username">The current user's username.</param>
    /// <param name="issuer">The current application name.</param>
    /// <param name="secretKey">The key used to create the QrCode.</param>
    /// <returns>A new instance of <see cref="QRCodeData"/>.</returns>
    QRCodeData GenerateQrCodeData(string username, string issuer, out string secretKey);

    /// <summary>
    /// Creates a QR code image.
    /// </summary>
    /// <param name="link">The qualified link. <code>otpauth://totp</code></param>
    /// <returns>A new instance of <see cref="QRCodeData"/>.</returns>
    QRCodeData GenerateQrCodeData(string link);

    /// <summary>
    /// Creates a Byte-Array containing raw PNG image
    /// </summary>
    /// <param name="data">The QrCode data to generate image from</param>
    /// <param name="pixelsPerModule">The pixel size.</param>
    /// <returns>The QR code image as a <see cref="byte"/> array</returns>
    byte[] GetGraphicAsByteArray(QRCodeData data, int pixelsPerModule = 20);

    /// <summary>
    /// Creates an otpauth link.
    /// </summary>
    /// <param name="username">The current user's username.</param>
    /// <param name="issuer">The current application name.</param>
    /// <param name="secretKey">The key used to create the QrCode.</param>
    /// <returns>A string representing the otpauth link.</returns>
    string GetTOTPLink(string username, string issuer, out string secretKey);

    /// <summary>
    /// Verifies the time-based one-time password (TOTP).
    /// </summary>
    /// <param name="secretKey">The secret key from the user.</param>
    /// <param name="token">The time-based one-time password.</param>
    /// <returns>True if the TOTP is valid.</returns>
    bool VerifyTOTP(string secretKey, string token);
}
