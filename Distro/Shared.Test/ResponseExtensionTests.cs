using gitViwe.Shared.Extension;
using Shared.Test.Data;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Shared.Test;

public class ResponseExtensionTests
{
    private readonly ITestOutputHelper _output;
    private readonly HttpClient _httpClient = new HttpClient();

    public ResponseExtensionTests(ITestOutputHelper output)
    {
        _output = output;
    }

    private static bool IsSuccessStatusCode(int statusCode) => (statusCode >= 200) && (statusCode <= 299);

    [Fact(Skip = "Run JWT API using docker")]
    public async Task Response_Unauthorized()
    {
        // make a get request to the API end point
        var requestUri = new Uri("http://localhost:5161/api/account/login");

        var requestResponse = await _httpClient.PostAsJsonAsync(requestUri, new { Email = "example@email.com", Password = "Password!" });

        var response = await requestResponse.ToProblemResponseAsync();

        _output.WriteLine(response.ToString());
        Assert.NotNull(response);

        Assert.False(IsSuccessStatusCode(response.Status ?? 0));
        Assert.True(response.Status == 401);
    }

    [Fact(Skip = "Run JWT API using docker")]
    public async Task Response_Validation()
    {
        // make a get request to the API end point
        var requestUri = new Uri("http://localhost:5161/api/account/login");

        var requestResponse = await _httpClient.PostAsJsonAsync(requestUri, new { Email = "example.com", Password = "Password" });

        var response = await requestResponse.ToValidationProblemResponseAsync();

        _output.WriteLine(response.ToString());
        Assert.NotNull(response);

        Assert.NotNull(response.Errors);
        Assert.False(IsSuccessStatusCode(response.Status ?? 0));
        Assert.True(response.Status == 400);
    }

    [Fact(Skip = "Run JWT API using docker")]
    public async Task Response_Success()
    {
        // make a get request to the API end point
        var requestUri = new Uri("http://localhost:5161/api/account/register");

        var requestResponse = await _httpClient.PostAsJsonAsync(requestUri, new { UserName = "User01", Email = "example@email.com", Password = "Password", PasswordConfirmation = "Password" });

        var response = await requestResponse.ToResponseAsync<TokenResponse>();

        _output.WriteLine(response.ToString());
        Assert.NotNull(response);

        Assert.False(string.IsNullOrWhiteSpace(response.Token));
        Assert.False(string.IsNullOrWhiteSpace(response.RefreshToken));
    }
}
