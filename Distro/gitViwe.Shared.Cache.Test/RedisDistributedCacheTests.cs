using gitViwe.Shared.Cache.Test.TestContainer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace gitViwe.Shared.Cache.Test;

public class RedisDistributedCacheTests : BaseIntegrationTest
{
    private readonly IHost _host;
    
    public RedisDistributedCacheTests(BaseIntegrationFixture baseIntegrationFixture)
        :base(baseIntegrationFixture)
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
                services.AddGitViweRedisCache(context.Configuration);
            })
            .Build();
    }
    
    [Fact]
    public async Task GetAsync_ReturnsTestValue_WhenSetAsyncKeyUsed()
    {
        // Arrange
        using IServiceScope serviceScope = _host.Services.CreateScope();
        var redisDistributedCache = serviceScope.ServiceProvider.GetRequiredService<IRedisDistributedCache>();
        var key = "testKey";
        
        // Act
        await redisDistributedCache.SetAsync(key, "testValue");
        var value = await redisDistributedCache.GetAsync(key);
        
        // Assert
        Assert.Equal("testValue", value);
    }
}
