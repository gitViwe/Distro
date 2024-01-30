﻿namespace gitViwe.Shared.Sqids;

/// <summary>
/// Implementation of the services registered in the DI container.
/// </summary>
public static class Startup
{
    /// <summary>
    /// Registers the default implementation for <see cref="ISqidsIdEncoder{T}"/> as <seealso cref="DefaultSqidsIdEncoder{T}"/>
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="options">The option values to use in <see cref="ISqidsIdEncoder{T}"/>.</param>
    /// <returns>An <see cref="IServiceCollection"/> to chain additional calls.</returns>
    public static IServiceCollection AddGitViweSqidsIdEncoder(this IServiceCollection services, Action<SqidsIdEncoderOption> options)
    {
        SqidsIdEncoderOption def = new();
        options(def);

        services.Configure(options)
            .AddSingleton(typeof(ISqidsIdEncoder<>), typeof(DefaultSqidsIdEncoder<>))
            .AddOptionsWithValidateOnStart<SqidsIdEncoderOption, SqidsIdEncoderOptionValidator>("SqidsIdEncoderOption")
            .ValidateOnStart();

        return services;
    }

    /// <summary>
    /// Registers implementation for <see cref="ISqidsIdEncoder{T}"/> as <typeparamref name="TImplementation"/> else defaults to <seealso cref="DefaultSqidsIdEncoder{T}"/>
    /// </summary>
    /// <typeparam name="TImplementation">The implementation to register.</typeparam>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="options">The option values to use in <see cref="ISqidsIdEncoder{T}"/>.</param>
    /// <returns>An <see cref="IServiceCollection"/> to chain additional calls.</returns>
    public static IServiceCollection AddGitViweSqidsIdEncoder<TImplementation>(this IServiceCollection services, Action<SqidsIdEncoderOption> options)
    {
        Type implementation = typeof(TImplementation);
        bool implementsInterface = typeof(ISqidsIdEncoder<>).MakeGenericType(implementation).IsAssignableFrom(implementation);

        if (implementsInterface)
        {
            services.AddSingleton(typeof(ISqidsIdEncoder<>), implementation.GetType());
            return services;
        }

        return services.AddGitViweSqidsIdEncoder(options);
    }
}
