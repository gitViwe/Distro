namespace gitViwe.Shared.Cache;

/// <summary>
/// Implementation of the services registered in the DI container.
/// </summary>
public static class Startup
{

    /// <summary>
    /// Registers the <see cref="IRedisDistributedCache"/> using values from <seealso cref="RedisDistributedCacheOption"/>.
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="options">The configuration options for Redis</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddGitViweRedisCache(this IServiceCollection services, Action<RedisDistributedCacheOption> options)
    {
        services.Configure(options)
            .AddStackExchangeRedisCache(options =>
            {
                options.Configuration = options.Configuration;
                options.InstanceName = options.InstanceName;
            })
            .AddScoped<IRedisDistributedCache, DefaultRedisDistributedCache>()
            .AddOptionsWithValidateOnStart<RedisDistributedCacheOption, RedisDistributedCacheOptionValidator>("RedisDistributedCacheOption", options);

        return services;
    }
}
