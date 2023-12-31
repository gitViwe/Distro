namespace gitViwe.Shared.Attribute;

/// <summary>
/// Marks a property that will hold the result of the decoded Sqids Id.
/// </summary>
[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public class DecodedSqidsIdAttribute : System.Attribute
{
    /// <summary>
    /// The property name that holds the encoded Sqids Id.
    /// </summary>
    public required string SourceProperty { get; init; }
}
