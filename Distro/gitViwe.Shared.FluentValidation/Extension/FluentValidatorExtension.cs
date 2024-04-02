namespace gitViwe.Shared.FluentValidation.Extension;

/// <summary>
/// Provides additional Fluent validation methods
/// </summary>
public static class FluentValidatorExtension
{
    /// <summary>
    /// Defines a Uri validator on the current rule builder, but only for string properties.
	/// Validation will fail if the value of the string is not a valid Uri.
    /// </summary>
    /// <typeparam name="T">Type of object being validated</typeparam>
	/// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> MustBeValidUri<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new UriAddressValidator<T>()).WithMessage("The address is not a valid Uri.");
    }

    /// <summary>
    /// Defines a Not contain validator on the current rule builder, but only for string properties.
    /// Validation will fail if the value of the string contains the characters defined in <paramref name="searchString"/>.
    /// </summary>
    /// <typeparam name="T">Type of object being validated</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
    /// <param name="searchString">The string or characters that must not be allowed</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> NotContain<T>(this IRuleBuilder<T, string> ruleBuilder, string searchString)
    {
        return ruleBuilder.SetValidator(new NotContainValidator<T>(searchString)).WithMessage("The string contains forbidden characters.");
    }
}
