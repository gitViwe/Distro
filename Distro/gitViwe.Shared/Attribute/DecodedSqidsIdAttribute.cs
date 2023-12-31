namespace gitViwe.Shared.Attribute;

/// <summary>
/// Marks a property that will hold the result of the decoded Sqids Id.
/// </summary>
[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public class DecodedSqidsIdAttribute : System.Attribute
{
    /// <summary>
    /// The encoded Sqids Id to decode.
    /// </summary>
    public required string EncodedSqidsId { get; init; }
}
