namespace gitViwe.Shared.FluentValidation.FluentValidators;

internal class NotContainValidator<T> : PropertyValidator<T, string>, INotContainValidator
{
    public override string Name => "NotContainValidator";

    public string SearchString { get; }

    public NotContainValidator(string searchString)
    {
        ArgumentException.ThrowIfNullOrEmpty(searchString, "SearchString should not be empty.");
        SearchString = searchString;
    }

    public override bool IsValid(ValidationContext<T> context, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return false == value.Contains(SearchString, StringComparison.InvariantCulture);
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return Localized(errorCode, Name);
    }
}

internal interface INotContainValidator : IPropertyValidator
{
    string SearchString { get; }
}
