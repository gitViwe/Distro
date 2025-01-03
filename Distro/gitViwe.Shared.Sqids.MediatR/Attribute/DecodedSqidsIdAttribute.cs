namespace gitViwe.Shared.Sqids.MediatR;

/// <summary>
/// Marks a property that will hold the result of the decoded Sqids Id.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class DecodedSqidsIdAttribute : Attribute
{
    /// <summary>
    /// The property name that holds the encoded Sqids Id.
    /// </summary>
    public required string SourceProperty { get; init; }
}
