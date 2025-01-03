namespace gitViwe.Shared.MongoDB;

/// <summary>
/// Attribute is used on classes and cannot be inherited
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class BsonCollectionAttribute(string collectionName) : Attribute
{
    /// <summary>
    /// The name of the MongoDB collection
    /// </summary>
    public string CollectionName { get; } = collectionName;
}
