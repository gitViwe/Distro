namespace gitViwe.Shared.MongoDB;

/// <summary>
/// Represents the base implementation of MongoDb documents
/// </summary>
public abstract class MongoDocument
{
    /// <summary>
    /// MongoDB document ID
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public ObjectId Id { get; init; } = ObjectId.GenerateNewId();
}
