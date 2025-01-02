using System.ComponentModel.DataAnnotations;

namespace gitViwe.Shared.Imgbb.Option;

/// <summary>
/// Configuration options for the Imgbb client
/// </summary>
public sealed class ImgBbClientOption
{
    /// <summary>
    /// The configuration values from the "ImgBBClientOption" section inside the appsettings.json file.
    /// </summary>
    public const string SectionName = "ImgBBClientOption";
    
    /// <summary>
    /// The API key.
    /// </summary>
    [Required]
    public string ApiKey { get; init; } = string.Empty;

    /// <summary>
    /// Enable this if you want to force uploads to be auto deleted after certain time (in seconds 60-15552000)
    /// </summary>
    [Required]
    [Range(60, 15552000, ErrorMessage = "Must be a value in the range of 60-15552000.")]
    public int ExpirationInSeconds { get; init; }
}