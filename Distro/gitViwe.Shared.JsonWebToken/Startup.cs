using gitViwe.Shared.JsonWebToken.Implementation;
using gitViwe.Shared.JsonWebToken.Option;

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
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddGitViweJsonWebToken(this IServiceCollection services)
    {
        services
            .AddOptionsWithValidateOnStart<JsonWebTokenOption>(null)
            .BindConfiguration(JsonWebTokenOption.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services.AddScoped<IJsonWebToken, DefaultJsonWebToken>();
    }
}
