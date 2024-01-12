using Microsoft.Extensions.Options;

namespace gitViwe.Shared.Imgbb.Option;

/// <summary>
/// Configuration options for the Imgbb client
/// </summary>
public class ImgBBClientOption
{
    /// <summary>
    /// The API key.
    /// </summary>
    public string APIKey { get; set; } = string.Empty;

    /// <summary>
    /// Enable this if you want to force uploads to be auto deleted after certain time (in seconds 60-15552000)
    /// </summary>
    public int? ExpirationInSeconds { get; set; }
}

internal class ImgBBClientOptionValidator : IValidateOptions<ImgBBClientOption>
{
    public ValidateOptionsResult Validate(string? name, ImgBBClientOption options)
    {
        if (string.IsNullOrWhiteSpace(options.APIKey))
        {
            return ValidateOptionsResult.Fail($"A value for {name}.{nameof(options.APIKey)} must be provided.");
        }

        if (options.ExpirationInSeconds.HasValue && options.ExpirationInSeconds is < 60 or > 15552000)
        {
            return ValidateOptionsResult.Fail($"{name}.{nameof(options.APIKey)} must be a value in the range of 60-15552000.");
        }

        return ValidateOptionsResult.Success;
    }
}