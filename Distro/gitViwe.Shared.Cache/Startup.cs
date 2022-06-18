using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace gitViwe.Shared.Cache;

/// <summary>
/// Implementation of the services registered in the DI container.
/// </summary>
public static class Startup
{
    /// <summary>
    /// Registers required cache services <see cref="IMemoryCacheService"/> and <see cref="IRedisCacheService"/>
    /// <br />Requires appsettings section <see cref="CacheConfiguration"/>
    /// </summary>
    public static IServiceCollection GitViweCacheServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // add 'CacheConfiguration' section to dependency container
        services.Configure<CacheConfiguration>(configuration.GetSection(nameof(CacheConfiguration)));

        // register services
        services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
        services.AddSingleton<IRedisCacheService, RedisCacheService>();

        return services;
    }

    /// <summary>
    /// Registers local storage service <see cref="ILocalStorageService"/>
    /// <br />Requires appsettings section <see cref="CacheConfiguration"/>
    /// </summary>
    public static IServiceCollection GitViweLocalStorageService(this IServiceCollection services)
    {
        // register services
        services.AddSingleton<ILocalStorageService, LocalStorageService>();

        return services;
    }
}
