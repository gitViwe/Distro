namespace gitViwe.Shared.JsonWebToken;

/// <summary>
/// Implementation of the services registered in the DI container.
/// </summary>
public static class Startup
{
    /// <summary>
    /// Registers the <see cref="IJsonWebToken"/> with default implementation of <seealso cref="DefaultJsonWebToken"/>
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="options">The option values to use in <see cref="IJsonWebToken"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddGitViweJsonWebToken(this IServiceCollection services, Action<JsonWebTokenOption> options)
    {
        services.AddScoped<IJsonWebToken, DefaultJsonWebToken>()
            .AddSingleton<IValidateOptions<JsonWebTokenOption>, JsonWebTokenOptionValidator>()
            .AddOptions<JsonWebTokenOption>("JsonWebTokenOption")
            .Configure(options)
            .ValidateOnStart();

        return services;
    }
}
