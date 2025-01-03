using gitViwe.Shared.Imgbb;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Shared.Test;

public class ImgBbClientTests
{
    private readonly IHost _host;
    
    public ImgBbClientTests()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((_, config) =>
            {
                config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();
            })
            .ConfigureServices((context, services) =>
            {
                if (Environment.GetEnvironmentVariable("TEST_IMGBBCLIENT_APIKEY") is string apiKey)
                {
                    context.Configuration["ImgBBClientOption:APIKey"] = apiKey;
                }
                services.AddGitViweImgBbClient(context.Configuration);
            })
            .Build();
    }
    
    [Fact]
    public async Task PingAsync_ShouldReturnSuccess_WhenServiceIsAvailable()
    {
        // Arrange
        using IServiceScope serviceScope = _host.Services.CreateScope();
        var imgBbClient = serviceScope.ServiceProvider.GetRequiredService<IImgBbClient>();

        // Act
        var response = await imgBbClient.PingAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.Succeeded);
    }
}
