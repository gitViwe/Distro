namespace gitViwe.Shared.Sqids;

/// <summary>
/// Implementation of the services registered in the DI container.
/// </summary>
public static class Startup
{
    /// <summary>
    /// Registers the default implementation for <see cref="ISqidsIdEncoder{T}"/> as <seealso cref="DefaultSqidsIdEncoder{T}"/>
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <returns>An <see cref="IServiceCollection"/> to chain additional calls.</returns>
    public static IServiceCollection AddGitViweSqidsIdEncoder(this IServiceCollection services)
    {
        services.AddSingleton(typeof(ISqidsIdEncoder<>), typeof(DefaultSqidsIdEncoder<>))
            .AddOptionsWithValidateOnStart<SqidsIdEncoderOption, SqidsIdEncoderOptionValidator>("SqidsIdEncoderOption");

        return services;
    }

    /// <summary>
    /// Registers the default implementation for <see cref="ISqidsIdEncoder{T}"/> as <seealso cref="DefaultSqidsIdEncoder{T}"/>
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="options">The option values to use in <see cref="ISqidsIdEncoder{T}"/>.</param>
    /// <returns>An <see cref="IServiceCollection"/> to chain additional calls.</returns>
    public static IServiceCollection AddGitViweSqidsIdEncoder(this IServiceCollection services, Action<SqidsIdEncoderOption> options)
    {
        services.Configure(options)
            .AddSingleton(typeof(ISqidsIdEncoder<>), typeof(DefaultSqidsIdEncoder<>))
            .AddOptionsWithValidateOnStart<SqidsIdEncoderOption, SqidsIdEncoderOptionValidator>("SqidsIdEncoderOption");

        return services;
    }

    /// <summary>
    /// Registers implementation for <see cref="ISqidsIdEncoder{T}"/> as <typeparamref name="TImplementation"/> else defaults to <seealso cref="DefaultSqidsIdEncoder{T}"/>
    /// </summary>
    /// <typeparam name="TImplementation">The implementation to register.</typeparam>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <returns>An <see cref="IServiceCollection"/> to chain additional calls.</returns>
    /// <exception cref="InvalidOperationException"
    public static IServiceCollection AddGitViweSqidsIdEncoder<TImplementation, TSqidsIdType>(this IServiceCollection services)
        where TImplementation : ISqidsIdEncoder<TSqidsIdType>
        where TSqidsIdType : unmanaged, IBinaryInteger<TSqidsIdType>, IMinMaxValue<TSqidsIdType>
    {
        return services.AddSingleton(typeof(ISqidsIdEncoder<>), typeof(TImplementation));
    }
}
