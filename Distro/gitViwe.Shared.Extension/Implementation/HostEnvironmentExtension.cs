using Microsoft.Extensions.Hosting;

namespace gitViwe.Shared.Extension;

/// <summary>
/// Provides extensions for the <see cref="IHostEnvironment"/>
/// </summary>
public static class HostEnvironmentExtension
{
    /// <summary>
    /// Checks if the current host environment name is <see cref="HubEnvironmentName.Docker"/>.
    /// </summary>
    /// <param name="environment">An instance of <see cref="IHostEnvironment"/>.</param>
    /// <returns>True if the environment name is <see cref="HubEnvironmentName.Docker"/>, otherwise false.</returns>
    public static bool IsDocker(this IHostEnvironment environment)
        => environment.IsEnvironment(HubEnvironmentName.Docker);

    /// <summary>
    /// Checks if the current host environment name is <see cref="HubEnvironmentName.Test"/>.
    /// </summary>
    /// <param name="environment">An instance of <see cref="IHostEnvironment"/>.</param>
    /// <returns>True if the environment name is <see cref="HubEnvironmentName.Test"/>, otherwise false.</returns>
    public static bool IsTest(this IHostEnvironment environment)
        => environment.IsEnvironment(HubEnvironmentName.Test);

    /// <summary>
    /// Checks if the current host environment name matches one of the <paramref name="environmentNames"/>.
    /// </summary>
    /// <param name="environment">An instance of <see cref="IHostEnvironment"/>.</param>
    /// <param name="environmentNames">The environment names to match.</param>
    /// <returns>True if the environment name matches one of the <paramref name="environmentNames"/>, otherwise false.</returns>
    public static bool IsAny(this IHostEnvironment environment, IEnumerable<string> environmentNames)
        => environmentNames.Contains(environment.EnvironmentName);
}
