namespace gitViwe.Shared.TimeBasedOneTimePassword;

internal class DefaultTimeBasedOneTimePassword(IOptions<TimeBasedOneTimePasswordOption> options) : ITimeBasedOneTimePassword
{
    private readonly TimeBasedOneTimePasswordOption _option = options.Value;

    public TimeBasedOneTimePasswordLinkResponse? GenerateLink(string username)
    {
        // Generate a random secret key for the user.
        var key = KeyGeneration.GenerateRandomKey(32);
        string secretKey = Base32Encoding.ToString(key);

        // Generate link
        string link = $"otpauth://totp/{username}" +
            $"?secret={secretKey}" +
            $"&issuer={_option.Issuer}" +
            $"&algorithm={_option.Algorithm}" +
            $"&digits={_option.Digits}" +
            $"&period={_option.Period}";

        return new TimeBasedOneTimePasswordLinkResponse(secretKey, link);
    }

    public bool VerifyToken(string secretKey, string token)
    {
        var key = Base32Encoding.ToBytes(secretKey);
        return new Totp(key).VerifyTotp(token, out _);
    }
}
