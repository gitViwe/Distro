using gitViwe.Shared.MongoDB.Option;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace gitViwe.Shared.MongoDB;

/// <summary>
/// Implementation of the services registered in the DI container.
/// </summary>
public static class Startup
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="options">The mongoDB client settings</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddGitViweMongoDBRepository(this IServiceCollection services, Action<MongoDBRepositoryOption> options)
    {
        services.Configure(options)
            .TryAddScoped(typeof(IMongoDBRepository<>), typeof(MongoDBRepository<>));
        return services;
    }
}
