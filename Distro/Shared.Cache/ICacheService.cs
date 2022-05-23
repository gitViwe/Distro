namespace Shared.Cache;

/// <summary>
/// Allows access to the selected <see cref="Enums"/>
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Attempts to get the value from the cache service
    /// </summary>
    /// <typeparam name="TResult">The data type of the object</typeparam>
    /// <param name="key">The identifier name for the object</param>
    /// <param name="data">The object type based on the key provided.</param>
    /// <returns>True if the data is found. False if the data is not found.</returns>
    Task<bool> TryGetAsync<TResult>(string key, out TResult data);

    /// <summary>
    /// Gets the value from the cache service
    /// </summary>
    /// <typeparam name="TResult">The data type of the object</typeparam>
    /// <param name="key">The identifier name for the object</param>
    /// <returns>The object type based on the key provided. Returns the object's default value if unsuccessful.</returns>
    Task<TResult> GetAsync<TResult>(string key);

    /// <summary>
    /// Store an object to the cache service
    /// </summary>
    /// <typeparam name="TData">The data type of the object</typeparam>
    /// <param name="key">The identifier name for the object</param>
    /// <param name="data">The object to store</param>
    /// <returns>The object type based on the key provided. Returns the object's default value if unsuccessful.</returns>
    Task<TData> SetAsync<TData>(string key, TData data);

    /// <summary>
    /// Delete an object from the cache service
    /// </summary>
    /// <param name="key">The identifier name for the object</param>
    Task RemoveAsync(string key);
}
