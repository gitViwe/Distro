using gitViwe.Shared;
using Shared.Test.Model;
using Shared.Test.TestDataGenerator;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Shared.Test;

public class ResultsWrapperTests(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;

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

    [Theory]
    [ClassData(typeof(PaginatedResponseTestData))]
    public void Success_PaginatedResponse_With_ValidData(IEnumerable<TokenResponse> data, int count, int page, int pageSize)
    {
        // Act
        var result = PaginatedResponse<TokenResponse>.Success(data, count, page, pageSize);

        // Assert
        _output.WriteLine($"PaginatedResponse.Data property must be of type: {data.GetType()}.");
        Assert.Equal(data, result.Data);
        _output.WriteLine($"PaginatedResponse.TotalCount property must have a value of: {count}.");
        Assert.Equal(count, result.TotalCount);
        _output.WriteLine($"PaginatedResponse.CurrentPage property must have a value of: {page}.");
        Assert.Equal(page, result.CurrentPage);
        _output.WriteLine($"PaginatedResponse.PageSize property must have a value of: {pageSize}.");
        Assert.Equal(pageSize, result.PageSize);
        _output.WriteLine($"PaginatedResponse.TotalPages property must have a value of: {(int)Math.Ceiling(count / (double)pageSize)}.");
        Assert.Equal((int)Math.Ceiling(count / (double)pageSize), result.TotalPages);
        _output.WriteLine($"PaginatedResponse.HasPreviousPage property must be false.");
        Assert.False(result.HasPreviousPage);
        _output.WriteLine($"PaginatedResponse.HasNextPage property must be true.");
        Assert.True(result.HasNextPage);
    }

    [Fact]
    public void Fail_PaginatedResponse_With_EmptyData()
    {
        // Act
        var result = PaginatedResponse<TokenResponse>.Fail();

        // Assert
        _output.WriteLine($"PaginatedResponse.Data property must be of empty.");
        Assert.Empty(result.Data);
        _output.WriteLine($"PaginatedResponse.TotalCount property must have a value of: {0}.");
        Assert.Equal(0, result.TotalCount);
        _output.WriteLine($"PaginatedResponse.CurrentPage property must have a value of: {1}.");
        Assert.Equal(1, result.CurrentPage);
        _output.WriteLine($"PaginatedResponse.PageSize property must have a value of: {15}.");
        Assert.Equal(15, result.PageSize);
        _output.WriteLine($"PaginatedResponse.TotalPages property must have a value of: {0}.");
        Assert.Equal(0, result.TotalPages);
        _output.WriteLine($"PaginatedResponse.HasPreviousPage property must be false.");
        Assert.False(result.HasPreviousPage);
        _output.WriteLine($"PaginatedResponse.HasNextPage property must be true.");
        Assert.False(result.HasNextPage);
    }
}
