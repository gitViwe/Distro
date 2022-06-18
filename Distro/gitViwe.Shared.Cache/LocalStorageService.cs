using Microsoft.JSInterop;
using System.Text.Json;

namespace gitViwe.Shared.Cache;

internal class LocalStorageService : ILocalStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public LocalStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<TResult> GetAsync<TResult>(string key, CancellationToken token = default) where TResult : class, new()
    {
        // run a JavaScript function to get item based on the 'key'
        var jsonResult = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", token, key);

        if (jsonResult is null)
        {
            // return default value of the object type
            return new TResult();
        }

        // deserialize JSON string to object type
        return JsonSerializer.Deserialize<TResult>(jsonResult);
    }

    public async Task RemoveAsync(string key, CancellationToken token = default)
    {
        // run a JavaScript function to delete item
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", token, key);
    }

    public async Task SetAsync<TData>(string key, TData data, CancellationToken token = default)
    {
        // run a JavaScript function to save item
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", token, key, JsonSerializer.Serialize(data));
    }
}