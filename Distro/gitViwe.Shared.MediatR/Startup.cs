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

    /// <summary>
    /// Registers the default implementation for <see cref="ISqidsIdEncoder{T}"/> as <seealso cref="SqidsIdEncoder{T}"/>
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <returns>An <see cref="IServiceCollection"/> to chain additional calls.</returns>
    public static IServiceCollection AddGitViweSqidsIdEncoder(this IServiceCollection services)
    {
        services.TryAddSingleton(typeof(ISqidsIdEncoder<>), typeof(SqidsIdEncoder<>));

        return services;
    }

    /// <summary>
    /// Registers implementation for <see cref="ISqidsIdEncoder{T}"/> as <typeparamref name="TImplementation"/> else defaults to <seealso cref="SqidsIdEncoder{T}"/>
    /// </summary>
    /// <typeparam name="TImplementation">The implementation to register.</typeparam>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <returns>An <see cref="IServiceCollection"/> to chain additional calls.</returns>
    public static IServiceCollection AddGitViweSqidsIdEncoder<TImplementation>(this IServiceCollection services)
    {
        Type implementation = typeof(TImplementation);
        bool implementsInterface = typeof(ISqidsIdEncoder<>).MakeGenericType(implementation).IsAssignableFrom(implementation);

        if (implementsInterface)
        {
            services.TryAddSingleton(typeof(ISqidsIdEncoder<>), implementation.GetType());
            return services;
        }

        return services.AddGitViweSqidsIdEncoder();
    }
}
