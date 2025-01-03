using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace gitViwe.Shared.TimeBasedOneTimePassword;

/// <summary>
/// The default option values for <seealso cref="ITimeBasedOneTimePassword"/>
/// </summary>
public sealed class TimeBasedOneTimePasswordOption
{
    /// <summary>
    /// The configuration values from the "TimeBasedOneTimePasswordOption" section inside the appsettings.json file.
    /// </summary>
    public const string SectionName = "TimeBasedOneTimePasswordOption";
    
    /// <summary>
    /// The issuer parameter is an optional string value indicating the provider or service the credential is associated with.
    /// It is URL-encoded according to <see href="https://datatracker.ietf.org/doc/html/rfc3986"/>
    /// </summary>
    [Required]
    [MinLength(3)]
    public string Issuer { get; init; } = string.Empty;

    /// <summary>
    /// The hash algorithm used by the credential.
    /// </summary>
    [Required]
    [DefaultValue(TimeBasedOneTimePasswordAlgorithm.SHA512)]
    public TimeBasedOneTimePasswordAlgorithm Algorithm { get; init; }

    /// <summary>
    /// The number of digits in a one-time password (OTP). Valid numbers are [6, 7, 8]
    /// </summary>
    [Required]
    [DefaultValue(8)]
    [Range(6, 8, ErrorMessage = "Valid numbers are [6, 7, 8]")]
    public ushort Digits { get; init; }

    /// <summary>
    /// The period parameter defines a validity period in seconds for the TOTP code. Valid numbers are [15, 30, 60]
    /// </summary>
    [Required]
    [DefaultValue(15)]
    [ValidValues<ushort>([15, 30, 60])]
    public ushort Period { get; init; }
}