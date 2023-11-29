using FluentValidation;
using gitViwe.Shared.FluentValidation.Extension;

namespace Shared.Test.Model;

internal class FluentValidatorExtensionTestObject
{
    public string UriProperty { get; set; } = string.Empty;
    public string StringProperty { get; set; } = string.Empty;
}

internal class FluentValidatorExtensionTestValidator : AbstractValidator<FluentValidatorExtensionTestObject>
{
    internal FluentValidatorExtensionTestValidator(string searchString)
    {
        RuleFor(x => x.UriProperty).MustBeValidUri();
        RuleFor(x => x.StringProperty).NotContain(searchString);
    }
}