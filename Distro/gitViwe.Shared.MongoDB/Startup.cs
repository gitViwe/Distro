namespace gitViwe.Shared.MongoDB;

/// <summary>
/// Implementation of the services registered in the DI container.
/// </summary>
public static class Startup
{
    /// <summary>
    /// Registers the <see cref="IMongoDBRepository{T}"/> with default implementation of <seealso cref="MongoDBRepository{T}"/>
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="options">The mongoDB client settings</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddGitViweMongoDBRepository(this IServiceCollection services, Action<MongoDBRepositoryOption> options)
    {
        services.Configure(options)
            .AddScoped(typeof(IMongoDBRepository<>), typeof(MongoDBRepository<>))
            .AddOptionsWithValidateOnStart<MongoDBRepositoryOption, MongoDBRepositoryOptionValidator>("MongoDBRepositoryOption", options);

        return services;
    }
}
