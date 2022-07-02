namespace gitViwe.Shared.Cache;

/// <summary>
/// Allows access to browser local storage
/// </summary>
public interface ILocalStorageService
{
    /// <summary>
    /// Get an object from local storage
    /// </summary>
    /// <typeparam name="TResult">The data type of the object</typeparam>
    /// <param name="key">The identifier name for the object</param>
    /// <param name="token">Propagates notification that operations should be canceled.</param>
    /// <returns>The object type based on the key provided. Returns the object's default value if unsuccessful.</returns>
    Task<TResult> GetAsync<TResult>(string key, CancellationToken token)

    /// <summary>
    /// Delete an object from local storage
    /// </summary>
    /// <param name="key">The identifier name for the object</param>
    /// <param name="token">Propagates notification that operations should be canceled.</param>
    Task RemoveAsync(string key, CancellationToken token);

    /// <summary>
    /// Store an object to local storage
    /// </summary>
    /// <typeparam name="TData">The data type of the object</typeparam>
    /// <param name="key">The identifier name for the object</param>
    /// <param name="data">The object to store</param>
    /// <param name="token">Propagates notification that operations should be canceled.</param>
    Task SetAsync<TData>(string key, TData data, CancellationToken token);
}
