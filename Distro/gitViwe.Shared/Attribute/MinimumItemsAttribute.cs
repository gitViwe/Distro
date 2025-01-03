using System.ComponentModel.DataAnnotations;

namespace gitViwe.Shared;

/// <summary>
/// Validated the collection to ensure it contains the minimum number of items.
/// </summary>
/// <param name="minItems">The minimum number of items in the collection.</param>
[AttributeUsage(AttributeTargets.Property)]
public sealed class MinimumItemsAttribute(int minItems)
    : ValidationAttribute($"The collection must contain at least {minItems} item(s).")
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
        // Ensure the value is of type IEnumerable<T> and if the collection has at least _minItems items
        if (value is IEnumerable<object> collection && collection.Count() >= minItems)
        {
            // Check if the collection has at least _minItems items
            return ValidationResult.Success;
        }

        // If the value isn't an IEnumerable<T>, validation fails
        return new ValidationResult(ErrorMessage, [validationContext.MemberName!]);
    }
}