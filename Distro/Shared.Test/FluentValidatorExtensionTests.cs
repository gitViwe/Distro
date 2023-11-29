using FluentValidation.TestHelper;
using Shared.Test.Model;
using Xunit;

namespace Shared.Test;

public class FluentValidatorExtensionTests
{
    [Fact]
    public void MustBeValidUri_ValidUri_ShouldNotHaveValidationError()
    {
        // Arrange
        FluentValidatorExtensionTestValidator validator = new("blank");

        // Act
        var result = validator.TestValidate(new FluentValidatorExtensionTestObject { UriProperty = "https://example.com" });

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.UriProperty);
    }

    [Fact]
    public void MustBeValidUri_InvalidUri_ShouldHaveValidationError()
    {
        // Arrange
        FluentValidatorExtensionTestValidator validator = new("blank");

        // Act
        var result = validator.TestValidate(new FluentValidatorExtensionTestObject { UriProperty = "not_a_valid_uri" });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UriProperty)
              .WithErrorMessage("The address is not a valid Uri.");
    }

    [Fact]
    public void NotContain_ContainsForbiddenCharacters_ShouldHaveValidationError()
    {
        // Arrange
        FluentValidatorExtensionTestValidator validator = new("forbidden");

        // Act
        var result = validator.TestValidate(new FluentValidatorExtensionTestObject { StringProperty = "contains forbidden" });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.StringProperty)
              .WithErrorMessage("The string contains forbidden characters.");
    }

    [Fact]
    public void NotContain_DoesNotContainForbiddenCharacters_ShouldNotHaveValidationError()
    {
        // Arrange
        FluentValidatorExtensionTestValidator validator = new("forbidden");

        // Act
        var result = validator.TestValidate(new FluentValidatorExtensionTestObject { StringProperty = "does not contain" });

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.StringProperty);
    }
}
