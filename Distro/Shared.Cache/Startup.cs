using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Shared.Cache;

public static class Startup
{
    public static IServiceCollection AddCustomCache(this IServiceCollection services, IConfiguration configuration)
    {
        // add 'CacheConfiguration' section to dependency container
        services.Configure<CacheConfiguration>(configuration.GetSection(nameof(CacheConfiguration)));

        // get the current value from the appsettings.json section
        services.AddSingleton(serviceProvider => serviceProvider.GetRequiredService<IOptions<CacheConfiguration>>().Value);

        // add In-Memory Caching
        services.AddMemoryCache();
        // register services
        services.AddTransient<MemoryCacheService>();
        services.AddTransient<RedisCacheService>();
        services.AddTransient<Func<CacheType, ICacheService>>(serviceProvider => key =>
        {
            switch (key)
            {
                case CacheType.Memory:
                    return serviceProvider.GetService<MemoryCacheService>();
                case CacheType.Redis:
                    return serviceProvider.GetService<RedisCacheService>();
                default:
                    return serviceProvider.GetService<MemoryCacheService>();
            }
        });

        return services;
    }
}
