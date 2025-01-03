using gitViwe.Shared.Sqids.Implementation;

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
        services
            .AddOptionsWithValidateOnStart<SqidsIdEncoderOption>(null)
            .BindConfiguration(SqidsIdEncoderOption.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        return services.AddSingleton(typeof(ISqidsIdEncoder<>), typeof(DefaultSqidsIdEncoder<>));
    }

    /// <summary>
    /// Registers implementation for <see cref="ISqidsIdEncoder{T}"/> as <typeparamref name="TImplementation"/> else defaults to <seealso cref="DefaultSqidsIdEncoder{T}"/>
    /// </summary>
    /// <typeparam name="TImplementation">The implementation to register.</typeparam>
    /// <typeparam name="TSqidsIdType">The sqids id data type.</typeparam>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <returns>An <see cref="IServiceCollection"/> to chain additional calls.</returns>
    public static IServiceCollection AddGitViweSqidsIdEncoder<TImplementation, TSqidsIdType>(this IServiceCollection services)
        where TImplementation : ISqidsIdEncoder<TSqidsIdType>
        where TSqidsIdType : unmanaged, IBinaryInteger<TSqidsIdType>, IMinMaxValue<TSqidsIdType>
    {
        return services.AddSingleton(typeof(ISqidsIdEncoder<>), typeof(TImplementation));
    }
}
