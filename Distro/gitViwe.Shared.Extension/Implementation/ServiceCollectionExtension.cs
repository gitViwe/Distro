namespace gitViwe.Shared.Extension;

/// <summary>
/// Provides wrapper extensions for the the <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// Adds, configures and validates the <typeparamref name="TOptions"/> values.
    /// </summary>
    /// <typeparam name="TOptions">The options type.</typeparam>
    /// <typeparam name="TValidateOptions">The options validator type.</typeparam>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="name">The name of the options instance.</param>
    /// <param name="options">The action used to configure the options.</param>
    /// <returns>A <see cref="OptionsBuilder{TOptions}"/> used to configure <typeparamref name="TOptions"/> instances.</returns>
    public static OptionsBuilder<TOptions> AddOptionsWithValidateOnStart<TOptions, TValidateOptions>(
        this IServiceCollection services,
        string name,
        Action<TOptions> options)
        where TOptions : class
        where TValidateOptions : class, IValidateOptions<TOptions>
    {
        return services.Configure(options)
            .AddSingleton<IValidateOptions<TOptions>, TValidateOptions>()
            .AddOptions<TOptions>(name)
            .Configure(options)
            .ValidateOnStart();
    }
}
