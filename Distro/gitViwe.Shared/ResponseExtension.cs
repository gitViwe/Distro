using System.Text.Json;
using System.Text.Json.Serialization;

namespace gitViwe.Shared
{
    /// <summary>
    /// Provides wrapper extensions for the the <see cref="HttpResponseMessage"/>
    /// </summary>
    public static class ResponseExtension
    {
        /// <summary>
        /// Process the HTTP response message into the wrapper class <see cref="Response"/>
        /// </summary>
        /// <typeparam name="TData">The data type returned from the response</typeparam>
        /// <param name="response">The HTTP response message from the API</param>
        /// <returns>The content of an HTTP response message as a <see cref="PaginatedResponse{TData}"/> model</returns>
        public static async Task<PaginatedResponse<TData>> ToPaginatedResponseAsync<TData>(this HttpResponseMessage response) where TData : class, new()
        {
            // get the response as a string
            var responseAsString = await response.Content.ReadAsStringAsync();

            // deserialize response into a 'Result' wrapper class
            var responseObject = JsonSerializer.Deserialize<PaginatedResponse<TData>>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            });

            // return the 'Result' wrapper class
            return responseObject;
        }

        /// <summary>
        /// Process the HTTP response message into the wrapper class <see cref="Response"/>
        /// </summary>
        /// <typeparam name="TData">The data type returned from the response</typeparam>
        /// <param name="response">The HTTP response message from the API</param>
        /// <returns>The content of an HTTP response message as a <see cref="Response{TData}"/> model</returns>
        public static async Task<IResponse<TData>> ToResponseAsync<TData>(this HttpResponseMessage response) where TData : class, new()
        {
            // get the response as a string
            var responseAsString = await response.Content.ReadAsStringAsync();

            // deserialize response into a 'Result' wrapper class
            var responseObject = JsonSerializer.Deserialize<Response<TData>>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            });

            // return the 'Result' wrapper class
            return responseObject;
        }

        /// <summary>
        /// Process the HTTP response message into the wrapper class <see cref="Response"/>
        /// </summary>
        /// <param name="response">The HTTP response message from the API</param>
        /// <returns>The content of an HTTP response message as a <see cref="Response"/> model</returns>
        public static async Task<IResponse> ToResponseAsync(this HttpResponseMessage response)
        {
            // get the response as a string
            var responseAsString = await response.Content.ReadAsStringAsync();

            // deserialize response into a 'Result' wrapper class
            var responseObject = JsonSerializer.Deserialize<Response>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            });

            // return the 'Result' wrapper class
            return responseObject;
        }
    }
}
