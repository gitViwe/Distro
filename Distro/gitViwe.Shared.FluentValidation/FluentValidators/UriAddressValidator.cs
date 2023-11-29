namespace gitViwe.Shared.FluentValidation.FluentValidators;

internal class UriAddressValidator<T> : PropertyValidator<T, string>
{
    public override string Name => "UriAddressValidator";

    public override bool IsValid(ValidationContext<T> context, string value)
    {
        return Uri.TryCreate(value, new UriCreationOptions(), out var _);
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return Localized(errorCode, Name);
    }
}
