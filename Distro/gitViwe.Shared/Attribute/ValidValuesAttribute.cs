using System.ComponentModel.DataAnnotations;

namespace gitViwe.Shared;

/// <summary>
/// Validated the value matches one of the values in the collection.
/// </summary>
/// <param name="validValues">The collection containing valid values.</param>
[AttributeUsage(AttributeTargets.Property)]
public sealed class ValidValuesAttribute<T>(T[] validValues)
    : ValidationAttribute($"Valid values are [{string.Join(',', validValues)}].")
{
    /// <summary>
    /// Protected virtual method to implement validation logic.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="validationContext">A ValidationContext instance that provides context about the validation operation,
    ///  such as the object and member being validated.</param>
    /// <returns>When validation is valid, ValidationResult. Success. When validation is invalid, an instance of ValidationResult.</returns>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return new ValidationResult(ErrorMessage, [validationContext.MemberName!]);
        }
        
        foreach (T validValue in validValues.Where(x => x is not null))
        {
            if (value is T currentValue && currentValue.Equals(validValue))
            {
                return ValidationResult.Success;
            }
        }
        
        return new ValidationResult(ErrorMessage, [validationContext.MemberName!]);
    }
}
