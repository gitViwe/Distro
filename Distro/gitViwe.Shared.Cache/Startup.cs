using gitViwe.Shared.Cache.Option;
using Microsoft.Extensions.DependencyInjection;

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
        var optionValue = new RedisDistributedCacheOption();
        options(optionValue);

        return services.Configure(options)
            .AddTransient<IRedisDistributedCache, RedisDistributedCache>()
            .AddStackExchangeRedisCache(options =>
            {
                options.Configuration = optionValue.Configuration;
                options.InstanceName = optionValue.InstanceName;
            });
    }
}
