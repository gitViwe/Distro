using gitViwe.Shared.Authentication.Option;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace gitViwe.Shared.Authentication;

/// <summary>
/// Implementation of the services registered in the DI container.
/// </summary>
public static class Startup
{
    /// <summary>
    /// Registers the <see cref="ISecurityTokenService"/> using values from <seealso cref="SecurityTokenOption"/>.
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="options">The configuration options for SecurityTokenService</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddGitViweSecurityTokenService(this IServiceCollection services, Action<SecurityTokenOption> options)
    {
        return services.Configure(options)
            .AddSingleton<IValidateOptions<SecurityTokenOption>, SecurityTokenOptionValidator>()
            .AddScoped<ISecurityTokenService, SecurityTokenService>();
    }

    /// <summary>
    /// Registers the <see cref="ITimeBasedOTPService"/> with default implementation of <seealso cref="TimeBasedOTPService"/>
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddGitViweTimeBasedOTPService(this IServiceCollection services)
    {
        services.TryAddTransient<ITimeBasedOTPService, TimeBasedOTPService>();
        return services;
    }
}
