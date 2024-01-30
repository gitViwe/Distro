namespace gitViwe.Shared.Sqids.Option;

public class SqidsIdEncoderOption
{
    /// <summary>
	/// Custom alphabet that will be used for the IDs.
	/// Must contain at least 5 characters.
	/// The default is lowercase letters, uppercase letters, and digits.
	/// </summary>
	public string Alphabet { get; set; } = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    /// <summary>
	/// The minimum length for the IDs.
	/// The default is 6; meaning the IDs will be as short as possible.
	/// 255 is the maximum.
	/// </summary>
	public int MinLength { get; set; } = 6;
}

internal class SqidsIdEncoderOptionValidator : IValidateOptions<SqidsIdEncoderOption>
{
    public ValidateOptionsResult Validate(string? name, SqidsIdEncoderOption options)
    {
        if (string.IsNullOrWhiteSpace(options.Alphabet))
        {
            return ValidateOptionsResult.Fail($"{name}.{nameof(options.Alphabet)} must be not be empty.");
        }

        if (options.MinLength < 6)
        {
            return ValidateOptionsResult.Fail($"{name}.{nameof(options.MinLength)} must be at least 6.");
        }

        return ValidateOptionsResult.Success;
    }
}