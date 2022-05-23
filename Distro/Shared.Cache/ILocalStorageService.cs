namespace Shared.Cache;

/// <summary>
/// Allows access to browser local storage
/// </summary>
public interface ILocalStorageService
{
    /// <summary>
    /// Attempts to get the value from local storage
    /// </summary>
    /// <typeparam name="TResult">The data type of the object</typeparam>
    /// <param name="key">The identifier name for the object</param>
    /// <param name="data">The object type based on the key provided.</param>
    /// <returns>True if the data is found. False if the data is not found.</returns>
    Task<bool> TryGetAsync<TResult>(string key, TResult data);

    /// <summary>
    /// Get an object from local storage
    /// </summary>
    /// <typeparam name="TResult">The data type of the object</typeparam>
    /// <param name="key">The identifier name for the object</param>
    /// <returns>The object type based on the key provided. Returns the object's default value if unsuccessful.</returns>
    Task<TResult> GetAsync<TResult>(string key);

    /// <summary>
    /// Delete an object from local storage
    /// </summary>
    /// <param name="key">The identifier name for the object</param>
    /// <returns></returns>
    Task RemoveAsync(string key);

    /// <summary>
    /// Store an object to local storage
    /// </summary>
    /// <typeparam name="TData">The data type of the object</typeparam>
    /// <param name="key">The identifier name for the object</param>
    /// <param name="data">The object to store</param>
    /// <returns></returns>
    Task SetAsync<TData>(string key, TData data);
}
