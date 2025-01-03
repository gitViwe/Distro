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
    /// <param name="configuration">Represents a set of key/ value application configuration properties.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddGitViweRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptionsWithValidateOnStart<RedisDistributedCacheOption>(null)
            .BindConfiguration(RedisDistributedCacheOption.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var redisDistributedCacheOption = configuration
            .GetSection(RedisDistributedCacheOption.SectionName)
            .Get<RedisDistributedCacheOption>();

        return services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisDistributedCacheOption!.Configuration;
            options.InstanceName = redisDistributedCacheOption.InstanceName;
        })
        .AddScoped<IRedisDistributedCache, DefaultRedisDistributedCache>();
    }
}
