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
    /// <param name="configuration">Represents a set of key/ value application configuration properties.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddGitViweJsonWebToken(this IServiceCollection services, IConfiguration configuration)
    {
        services.
            .AddSingleton<IValidateOptions<JsonWebTokenOption>, JsonWebTokenOptionValidator>()
            .AddOptions<JsonWebTokenOption>()
            .BindConfiguration(JsonWebTokenOption.SectionName)
            .ValidateOnStart();

        return services;
    }
}
