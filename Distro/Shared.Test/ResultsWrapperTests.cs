using gitViwe.Shared;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Shared.Test;

public class ResultsWrapperTests
{
    private readonly ITestOutputHelper _output;

    public ResultsWrapperTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Fail_With_400_StatusCode()
    {
        var result = Response.Fail("We encountered a failed response.");

        _output.WriteLine($"'Result' object must not be null.");
        Assert.NotNull(result);

        _output.WriteLine($"'Succeeded' property must have false value.");
        Assert.False(result.Succeeded);

        _output.WriteLine($"'StatusCode' property must contain 400.");
        Assert.True(result.StatusCode == 400);
    }

    [Theory]
    [InlineData("An unhandled error has occurred.", 404)]
    [InlineData("An unhandled error has occurred.", 500)]
    public void Fail_With_Custom_StatusCode(string message, int statusCode)
    {
        var result = Response.Fail(message, statusCode);

        _output.WriteLine($"'Result' object must not be null.");
        Assert.NotNull(result);

        _output.WriteLine($"'Succeeded' property must have false value.");
        Assert.False(result.Succeeded);

        _output.WriteLine($"'Message' property must contain {message}.");
        Assert.True(result.Message == message);

        _output.WriteLine($"'StatusCode' property must contain {statusCode}.");
        Assert.True(result.StatusCode == statusCode);
    }

    [Theory]
    [InlineData("An unhandled error has occurred.", 202)]
    [InlineData("An unhandled error has occurred.", 203)]
    public void Fail_With_Exception(string message, int statusCode)
    {
        _output.WriteLine($"'Result' object must throw an ArgumentException.");
        Assert.Throws<ArgumentException>(() => Response.Fail(message, statusCode));
    }

    [Fact]
    public void Success_With_200_StatusCode()
    {
        var result = Response.Success("We encountered a failed response.");

        _output.WriteLine($"'Result' object must not be null.");
        Assert.NotNull(result);

        _output.WriteLine($"'Succeeded' property must have false value.");
        Assert.True(result.Succeeded);

        _output.WriteLine($"'StatusCode' property must contain 200.");
        Assert.True(result.StatusCode == 200);
    }

    [Theory]
    [InlineData("Request was successful.", 202)]
    [InlineData("Request was successful.", 203)]
    public void Success_With_Custom_StatusCode(string message, int statusCode)
    {
        var result = Response.Success(message, statusCode);

        _output.WriteLine($"'Result' object must not be null.");
        Assert.NotNull(result);

        _output.WriteLine($"'Succeeded' property must have false value.");
        Assert.True(result.Succeeded);

        _output.WriteLine($"'Message' property must contain {message}.");
        Assert.True(result.Message == message);

        _output.WriteLine($"'StatusCode' property must contain {statusCode}.");
        Assert.True(result.StatusCode == statusCode);
    }

    [Theory]
    [InlineData("Request was successful.", 512)]
    [InlineData("Request was successful.", 403)]
    public void Success_With_Exception(string message, int statusCode)
    {
        _output.WriteLine($"'Result' object must throw an ArgumentException.");
        Assert.Throws<ArgumentException>(() => Response.Success(message, statusCode));
    }
}
