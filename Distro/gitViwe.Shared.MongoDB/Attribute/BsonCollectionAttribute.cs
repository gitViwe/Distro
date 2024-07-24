namespace gitViwe.Shared.MongoDB;

/// <summary>
/// Attribute is used on classes and cannot be inherited
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class BsonCollectionAttribute : System.Attribute
{
    /// <summary>
    /// This attribute allows us to set the collection name for documents
    /// </summary>
    public BsonCollectionAttribute(string collectionName)
    {
        CollectionName = collectionName;
    }

    /// <summary>
    /// The name of the MongoDB collection
    /// </summary>
    public string CollectionName { get; }
}
