namespace gitViwe.Shared.Imgbb.Option;

/// <summary>
/// Configuration options for the Imgbb client
/// </summary>
public class ImgBbClientOption
{
    /// <summary>
    /// The configuration values from the "ImgBBClientOption" section inside the appsettings.json file.
    /// </summary>
    public const string SectionName = "ImgBBClientOption";
    
    /// <summary>
    /// The API key.
    /// </summary>
    public string ApiKey { get; init; } = string.Empty;

    /// <summary>
    /// Enable this if you want to force uploads to be auto deleted after certain time (in seconds 60-15552000)
    /// </summary>
    public int? ExpirationInSeconds { get; set; }
}

internal class ImgBbClientOptionValidator : IValidateOptions<ImgBbClientOption>
{
    public ValidateOptionsResult Validate(string? name, ImgBbClientOption options)
    {
        if (string.IsNullOrWhiteSpace(options.ApiKey))
        {
            return ValidateOptionsResult.Fail($"A value for {name}.{nameof(options.ApiKey)} must be provided.");
        }

        if (options.ExpirationInSeconds.HasValue && options.ExpirationInSeconds is < 60 or > 15552000)
        {
            return ValidateOptionsResult.Fail($"{name}.{nameof(options.ExpirationInSeconds)} must be a value in the range of 60-15552000.");
        }

        return ValidateOptionsResult.Success;
    }
}