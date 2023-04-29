using QRCoder;
using OtpNet;

namespace gitViwe.Shared.Authentication;

internal class TimeBasedOTPService : ITimeBasedOTPService
{
    public QRCodeData GenerateQrCodeData(string username, string issuer, out string secretKey)
    {
        ArgumentNullException.ThrowIfNull(nameof(username));
        ArgumentNullException.ThrowIfNull(nameof(issuer));

        // Generate a random secret key for the user.
        var key = KeyGeneration.GenerateRandomKey(32);
        secretKey = Base32Encoding.ToString(key);

        return new QRCodeGenerator().CreateQrCode(
            $"otpauth://totp/{username}" +
            $"?secret={secretKey}" +
            $"&issuer={issuer}",
            QRCodeGenerator.ECCLevel.Q);
    }

    public byte[] GetGraphicAsByteArray(QRCodeData data, int pixelsPerModule = 20)
    {
        return new PngByteQRCode(data).GetGraphic(pixelsPerModule);
    }

    public bool VerifyTOTP(string secretKey, string token)
    {
        var key = Base32Encoding.ToBytes(secretKey);
        return new Totp(key).VerifyTotp(token, out _);
    }
}
