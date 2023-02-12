using gitViwe.ProblemDetail.Base;
using gitViwe.Shared;
using gitViwe.Shared.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Shared.Test.Data;
using Shared.Test.TestDataGenerator;
using SoloX.CodeQuality.Test.Helpers.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Shared.Test;

public class ResponseExtensionTests
{
    private readonly ITestOutputHelper _output;
    private readonly HttpClientMockBuilder _clientMockBuilder = new HttpClientMockBuilder();

    public ResponseExtensionTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [ClassData(typeof(ResponseExtensionTestProblemDetailData))]
    public async Task Response_ProblemDetail(Uri requestUri, HttpStatusCode statusCode, DefaultProblemDetails content, HttpMethod method, object requestBody)
    {
        var httpClient = _clientMockBuilder
            .WithBaseAddress(new Uri(requestUri.AbsoluteUri.Replace(requestUri.AbsolutePath, string.Empty)))
            .WithJsonContentRequest<ITokenRequest>(requestUri.AbsolutePath, method)
            .RespondingJsonContent(content, statusCode)
            .Build();

        var requestResponse = await httpClient.PostAsJsonAsync(requestUri.AbsolutePath, requestBody);

        var response = await requestResponse.ToProblemResponseAsync();

        Assert.NotNull(response);
        Assert.False(requestResponse.IsSuccessStatusCode);
        Assert.True(response.Status == (int)statusCode);

        _output.WriteLine(response.ToString());
    }

    [Theory]
    [ClassData(typeof(ResponseExtensionTestValidationProblemDetailData))]
    public async Task Response_Validation(Uri requestUri, HttpStatusCode statusCode, ValidationProblemDetails content, HttpMethod method, object requestBody)
    {
        var httpClient = _clientMockBuilder
            .WithBaseAddress(new Uri(requestUri.AbsoluteUri.Replace(requestUri.AbsolutePath, string.Empty)))
            .WithJsonContentRequest<ITokenRequest>(requestUri.AbsolutePath, method)
            .RespondingJsonContent(content, statusCode)
            .Build();

        var requestResponse = await httpClient.PostAsJsonAsync(requestUri.AbsolutePath, requestBody);

        var response = await requestResponse.ToValidationProblemResponseAsync();

        Assert.NotNull(response);
        Assert.False(requestResponse.IsSuccessStatusCode);
        Assert.True(response.Status == (int)statusCode);

        _output.WriteLine(response.ToString());
    }

    [Fact]
    public async Task Response_Success()
    {
        var requestUri = new Uri("http://localhost:5161/api/account/register");
        var content = new TokenResponse()
        {
            RefreshToken = Generator.RandomString(),
            Token = Generator.RandomString()
        };

        var httpClient = _clientMockBuilder
            .WithBaseAddress(new Uri(requestUri.AbsoluteUri.Replace(requestUri.AbsolutePath, string.Empty)))
            .WithJsonContentRequest<ITokenRequest>(requestUri.AbsolutePath, HttpMethod.Post)
            .RespondingJsonContent(content, HttpStatusCode.OK)
            .Build();

        var requestResponse = await httpClient.PostAsJsonAsync(requestUri, new { UserName = "User01", Email = "example@email.com", Password = "Password", PasswordConfirmation = "Password" });

        var response = await requestResponse.ToResponseAsync<TokenResponse>();

        Assert.NotNull(response);
        Assert.False(string.IsNullOrWhiteSpace(response.Token));
        Assert.False(string.IsNullOrWhiteSpace(response.RefreshToken));

        _output.WriteLine(response.ToString());
    }
}
