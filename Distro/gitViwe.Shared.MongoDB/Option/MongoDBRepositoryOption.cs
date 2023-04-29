using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace gitViwe.Shared.MongoDB.Option;

/// <summary>
/// Configuration options for creating a MongoDB repository.
/// </summary>
public class MongoDBRepositoryOption
{
    /// <summary>
    /// The settings for the mongoDb client
    /// </summary>
    public MongoClientSettings MongoClientSettings { get; set; }

    /// <summary>
    /// The mongoDb database name
    /// </summary>
    public string DatabaseName { get; set; } = string.Empty;
}

internal class MongoDBRepositoryOptionValidator : IValidateOptions<MongoDBRepositoryOption>
{
    public ValidateOptionsResult Validate(string? name, MongoDBRepositoryOption options)
    {
        if (string.IsNullOrWhiteSpace(options.DatabaseName))
        {
            return ValidateOptionsResult.Fail($"A value for {name}.{nameof(options.DatabaseName)} must be provided.");
        }

        if (options.MongoClientSettings is null)
        {
            return ValidateOptionsResult.Fail($"{name}.{nameof(options.MongoClientSettings)} must be provided.");
        }

        return ValidateOptionsResult.Success;
    }
}