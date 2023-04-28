using gitViwe.Shared.Authentication.Option;
using Microsoft.Extensions.DependencyInjection;

namespace gitViwe.Shared.Authentication;

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
            .AddScoped<ISecurityTokenService, SecurityTokenService>();
    }
}
