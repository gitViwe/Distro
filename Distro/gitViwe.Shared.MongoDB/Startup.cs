using gitViwe.Shared.MongoDB.Implementation;

namespace gitViwe.Shared.MongoDB;

/// <summary>
/// Implementation of the services registered in the DI container.
/// </summary>
public static class Startup
{
    /// <summary>
    /// Registers the <see cref="IMongoDbRepository{TMongoDocument}"/> with default implementation of <seealso cref="MongoDbRepository{TMongoDocument}"/>
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddGitViweMongoDbRepository(this IServiceCollection services)
    {
        services
            .AddOptionsWithValidateOnStart<MongoDbRepositoryOption>(null)
            .BindConfiguration(MongoDbRepositoryOption.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services.AddScoped(typeof(IMongoDbRepository<>), typeof(MongoDbRepository<>));
    }
}
