using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace gitViwe.ProblemDetail;

/// <summary>
/// Registers the services required for problem details factory
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Registers the <see cref="IProblemDetailFactory"/> implementation
    /// </summary>
    /// <returns>THe <see cref="IServiceCollection"/> to chain more calls together.</returns>
    public static IServiceCollection AddGitViweProblemDetailFactory(this IServiceCollection services)
    {
        services.TryAddSingleton<IProblemDetailFactory, ProblemDetailFactory>();
        return services;
    }
}
