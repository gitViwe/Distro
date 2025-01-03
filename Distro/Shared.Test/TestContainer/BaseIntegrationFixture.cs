using System.Threading.Tasks;
using Testcontainers.Redis;
using Xunit;
using IContainer = DotNet.Testcontainers.Containers.IContainer;

namespace Shared.Test.TestContainer;

public sealed class BaseIntegrationFixture : IAsyncLifetime
{
    private static readonly IContainer _redisContainer = new RedisBuilder()
        .WithImage("redis:7.0")
        .WithName("redis-cache")
        .WithPortBinding(6379, 6379)
        .Build();
    
    public async Task InitializeAsync()
    {
        await _redisContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _redisContainer.StopAsync();
    }
}

[CollectionDefinition(nameof(BaseIntegrationFixtureCollection))]
public sealed class BaseIntegrationFixtureCollection : ICollectionFixture<BaseIntegrationFixture> { }

[Collection(nameof(BaseIntegrationFixtureCollection))]
public class BaseIntegrationTest(BaseIntegrationFixture integrationFixture)
{
    public BaseIntegrationFixture IntegrationFixture { get; } = integrationFixture;
}