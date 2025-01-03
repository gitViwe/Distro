using gitViwe.Shared.Imgbb.Implementation;
using Microsoft.Extensions.Configuration;

namespace gitViwe.Shared.Imgbb;

/// <summary>
/// Implementation of the services registered in the DI container.
/// </summary>
public static class Startup
{
    /// <summary>
    /// Registers the <see cref="IImgBbClient"/> using values from <seealso cref="ImgBbClientOption"/>.
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="configuration">Represents a set of key/ value application configuration properties.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddGitViweImgBbClient(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptionsWithValidateOnStart<ImgBbClientOption>(null)
            .BindConfiguration(ImgBbClientOption.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        var imgBbClientOption = configuration
            .GetSection(ImgBbClientOption.SectionName)
            .Get<ImgBbClientOption>();

        services.AddHttpClient<IImgBbClient, DefaultImgBbClient>(client => client.BaseAddress = new Uri(imgBbClientOption!.BaseAddress));

        return services;
    }

    /// <summary>
    /// Registers the <see cref="IImgBbClient"/> with the mock <seealso cref="LocalMockClient"/>.
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddGitViweImgBbClientMock(this IServiceCollection services)
    {
        return services.AddSingleton<IImgBbClient, LocalMockClient>();
    }
}
