namespace gitViwe.Shared.Cache;

/// <summary>
/// Allows access to memory cache
/// </summary>
public interface IMemoryCacheService
{
    /// <summary>
    /// Gets the value from the cache service
    /// </summary>
    /// <typeparam name="TResult">The data type of the object</typeparam>
    /// <param name="key">The identifier name for the object</param>
    /// <returns>The object type based on the key provided. Returns the object's default value if unsuccessful.</returns>
    Task<TResult> GetAsync<TResult>(string key) where TResult : class, new();

    /// <summary>
    /// Store an object to the cache service
    /// </summary>
    /// <typeparam name="TData">The data type of the object</typeparam>
    /// <param name="key">The identifier name for the object</param>
    /// <param name="data">The object to store</param>
    /// <returns>The object type based on the key provided. Returns the object's default value if unsuccessful.</returns>
    Task SetAsync<TData>(string key, TData data);

    /// <summary>
    /// Delete an object from the cache service
    /// </summary>
    /// <param name="key">The identifier name for the object</param>
    Task RemoveAsync(string key);
}
