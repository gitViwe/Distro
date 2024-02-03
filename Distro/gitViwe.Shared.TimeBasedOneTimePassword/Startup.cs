namespace gitViwe.Shared.TimeBasedOneTimePassword;

/// <summary>
/// Implementation of the services registered in the DI container.
/// </summary>
public static class Startup
{
    /// <summary>
    /// Registers the <see cref="ITimeBasedOneTimePassword"/> with default implementation of <seealso cref="DefaultTimeBasedOneTimePassword"/>
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="options">The option values to use in <see cref="ITimeBasedOneTimePassword"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddGitViweTimeBasedOneTimePassword(this IServiceCollection services, Action<TimeBasedOneTimePasswordOption> options)
    {
        services.Configure(options)
            .AddScoped<ITimeBasedOneTimePassword, DefaultTimeBasedOneTimePassword>()
            .AddOptionsWithValidateOnStart<TimeBasedOneTimePasswordOption, TimeBasedOneTimePasswordOptionValidator>("TimeBasedOneTimePasswordOption", options);

        return services;
    }
}
