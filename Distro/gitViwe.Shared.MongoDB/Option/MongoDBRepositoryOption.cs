namespace gitViwe.Shared.MongoDB;

/// <summary>
/// Configuration options for creating a MongoDB repository.
/// </summary>
public sealed class MongoDbRepositoryOption
{
    /// <summary>
    /// The configuration values from the "MongoDbRepositoryOption" section inside the appsettings.json file.
    /// </summary>
    public const string SectionName = "MongoDbRepositoryOption";
    
    /// <summary>
    /// The settings for the mongoDb client
    /// </summary>
    public MongoClientSettings MongoClientSettings => MongoClientSettings.FromConnectionString(ConnectionString);

    /// <summary>
    /// The mongoDb database name
    /// </summary>
    [Required]
    public string DatabaseName { get; init; } = string.Empty;
    
    /// <summary>
    /// The mongoDb database connection string
    /// </summary>
    [Required]
    public string ConnectionString { get; init; } = string.Empty;
}
