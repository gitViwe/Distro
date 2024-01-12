﻿using gitViwe.Shared.Imgbb.Option;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
            .AddSingleton<IValidateOptions<ImgBBClientOption>, ImgBBClientOptionValidator>()
            .AddHttpClient<IImgBBClient, ImgBBClient>(client =>
            {
                client.BaseAddress = new Uri("https://api.imgbb.com");
            });

        return services;
    }
}