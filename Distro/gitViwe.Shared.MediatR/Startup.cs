namespace gitViwe.Shared.MediatR;

/// <summary>
/// Implementation of the services registered in the DI container.
/// </summary>
public static class Startup
{
    /// <summary>
    /// Configures values for <see cref="OpenTelemetryBehaviourOption"/> that will be used in the following behaviours:
    /// <br /> <see cref="OpenTelemetryPreProcessor{TRequest}"/>
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="options">The configuration options for the MediatR behaviour</param>
    /// <returns>An <see cref="IServiceCollection"/> to chain additional calls</returns>
    public static IServiceCollection ConfigureGitViweOpenTelemetryBehaviourOption(
        this IServiceCollection services,
        Action<OpenTelemetryBehaviourOption> options)
    {
        return services.Configure(options);
    }
}
