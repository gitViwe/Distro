namespace gitViwe.Shared.Sqids;

/// <summary>
/// Optional values used to create a <see cref="SqidsEncoder{T}"/>
/// </summary>
public sealed class SqidsIdEncoderOption
{
    /// <summary>
    /// The configuration values from the "SqidsIdEncoderOption" section inside the appsettings.json file.
    /// </summary>
    public const string SectionName = "SqidsIdEncoderOption";
    
    /// <summary>
	/// Custom alphabet that will be used for the IDs.
	/// Must contain at least 5 characters.
	/// The default is lowercase letters, uppercase letters, and digits.
	/// </summary>
	[Required]
    [MinLength(5, ErrorMessage = "Min length is 5")]
	public string Alphabet { get; init; } = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    /// <summary>
	/// The minimum length for the IDs.
	/// The default is 6; meaning the IDs will be as short as possible.
	/// 255 is the maximum.
	/// </summary>
	[Required]
    [DefaultValue(6)]
    [Range(6, 255, ErrorMessage = "Value must be between 6 and 255.")]
	public int MinLength { get; init; }
}
