using System.ComponentModel.DataAnnotations;

namespace gitViwe.Shared;

/// <summary>
/// Validation will fail if the value of the string contains the characters defined in <paramref name="searchString"/>.
/// </summary>
/// <param name="searchString">The string or characters that must not be allowed</param>
[AttributeUsage(AttributeTargets.Property)]
public sealed class DoesNotContainAttribute(string searchString)
    : ValidationAttribute($"{0} must not contain '{searchString}'.")
{
    /// <inheritdoc/>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // ValidationAttributes should generally return Success for null values
        // Leave null checking to the [Required] attribute
        if (value is null)
        {
            return ValidationResult.Success;
        }

        if (value is string stringValue)
        {
            bool containsSearchString = stringValue.Contains(searchString, StringComparison.InvariantCulture);

            if (containsSearchString)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }

        // Return success or throw if the attribute is applied to a non-string type
        return ValidationResult.Success;
    }

    /// <inheritdoc/>
    public override string FormatErrorMessage(string name)
    {
        return string.Format(System.Globalization.CultureInfo.CurrentCulture, ErrorMessageString, name, searchString);
    }
}