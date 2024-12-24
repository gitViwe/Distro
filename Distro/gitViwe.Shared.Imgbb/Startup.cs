namespace gitViwe.Shared.Imgbb;

/// <summary>
/// Implementation of the services registered in the DI container.
/// </summary>
public static class Startup
{
    /// <summary>
    /// Registers the <see cref="IImgBBClient"/> using values from <seealso cref="ImgBbClientOption"/>.
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="options">The configuration options for the ImgBBClient</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddGitViweImgBBClient(this IServiceCollection services)
    {
        services
            .AddSingleton<IValidateOptions<ImgBbClientOption>, ImgBbClientOptionValidator>()
            .AddOptions<ImgBbClientOption>()
            .BindConfiguration(ImgBbClientOption.SectionName)
            .ValidateOnStart();

        services.AddHttpClient<IImgBBClient, DefaultImgBBClient>(client => client.BaseAddress = new Uri("https://api.imgbb.com"));

        return services;
    }

    /// <summary>
    /// Registers the <see cref="IImgBBClient"/> with the mock <seealso cref="LocalMockClient"/>.
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddGitViweImgBBClientMock(this IServiceCollection services)
    {
        return services.AddSingleton<IImgBBClient, LocalMockClient>();
    }
}
