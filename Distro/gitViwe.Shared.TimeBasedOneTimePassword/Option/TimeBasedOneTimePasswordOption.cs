namespace gitViwe.Shared.TimeBasedOneTimePassword;

/// <summary>
/// The default option values for <seealso cref="ITimeBasedOneTimePassword"/>
/// </summary>
public class TimeBasedOneTimePasswordOption
{
    /// <summary>
    /// The issuer parameter is an optional string value indicating the provider or service the credential is associated with.
    /// It is URL-encoded according to <see href="https://datatracker.ietf.org/doc/html/rfc3986"/>
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// The hash algorithm used by the credential. The default is <see cref="TimeBasedOneTimePasswordAlgorithm.SHA1"/>.
    /// </summary>
    public TimeBasedOneTimePasswordAlgorithm Algorithm { get; set; } = TimeBasedOneTimePasswordAlgorithm.SHA1;

    /// <summary>
    /// The number of digits in a one-time password (OTP). Valid numbers are [6, 7, 8]
    /// </summary>
    public ushort Digits { get; set; } = 6;

    /// <summary>
    /// The period parameter defines a validity period in seconds for the TOTP code. Valid numbers are [15, 30, 60]
    /// </summary>
    public ushort Period { get; set; } = 30;
}

internal class TimeBasedOneTimePasswordOptionValidator : IValidateOptions<TimeBasedOneTimePasswordOption>
{
    public ValidateOptionsResult Validate(string? name, TimeBasedOneTimePasswordOption options)
    {
        name = typeof(TimeBasedOneTimePasswordOption).Name;

        if (string.IsNullOrWhiteSpace(options.Issuer))
        {
            return ValidateOptionsResult.Fail($"{name}.{nameof(options.Issuer)} must be provided.");
        }

        if (false == Enum.IsDefined(options.Algorithm))
        {
            return ValidateOptionsResult.Fail($"{name}.{nameof(options.Algorithm)} is not valid.");
        }

        if (false == (options.Digits == 6 || options.Digits == 7 || options.Digits == 8))
        {
            return ValidateOptionsResult.Fail($"{name}.{nameof(options.Digits)} must be 6, 7 or 8.");
        }

        if (false == (options.Period == 15 || options.Period == 30 || options.Period == 60))
        {
            return ValidateOptionsResult.Fail($"{name}.{nameof(options.Digits)} must be 15, 30 or 60.");
        }

        return ValidateOptionsResult.Success;
    }
}