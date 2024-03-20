using gitViwe.Shared.Imgbb;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Shared.Test;

public class ImgBBClientTests
{
    private readonly IServiceProvider _serviceProvider;

    public ImgBBClientTests()
    {
        _serviceProvider = new ServiceCollection()
            .AddGitViweImgBBClientMock()
            .BuildServiceProvider();
    }

    [Fact]
    public async Task PingAsync_ReturnsTrue()
    {
        // Arrange
        var client = _serviceProvider.GetRequiredService<IImgBBClient>();

        // Act
        var result = await client.PingAsync(CancellationToken.None);

        // Assert
        Assert.True(result);
    }
}
