namespace gitViwe.Shared.Imgbb;

/// <summary>
/// Implementation of the services registered in the DI container.
/// </summary>
public static class Startup
{
    /// <summary>
    /// Registers the <see cref="IImgBBClient"/> using values from <seealso cref="ImgBBClientOption"/>.
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="options">The configuration options for the ImgBBClient</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddGitViweImgBBClient(this IServiceCollection services, Action<ImgBBClientOption> options)
    {
        services.Configure(options)
            .AddScoped<IImgBBClient, DefaultImgBBClient>()
            .AddOptionsWithValidateOnStart<ImgBBClientOption, ImgBBClientOptionValidator>("ImgBBClientOption");

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
