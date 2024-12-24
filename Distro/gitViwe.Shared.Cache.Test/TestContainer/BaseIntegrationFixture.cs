using DotNet.Testcontainers.Containers;
using Testcontainers.Redis;

namespace gitViwe.Shared.Cache.Test.TestContainer;

public class BaseIntegrationFixture : IAsyncLifetime
{
    protected static readonly IContainer RedisContainer = new RedisBuilder()
        .WithImage("redis:7.0")
        .WithName("redis-cache")
        .WithPortBinding(6379, 6379)
        .Build();
    
    public async Task InitializeAsync()
    {
        await RedisContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await RedisContainer.StopAsync();
    }
}

[CollectionDefinition(nameof(BaseIntegrationFixtureCollection))]
public class BaseIntegrationFixtureCollection : ICollectionFixture<BaseIntegrationFixture> { }

[Collection(nameof(BaseIntegrationFixtureCollection))]
public class BaseIntegrationTest(BaseIntegrationFixture integrationFixture)
{
    public BaseIntegrationFixture IntegrationFixture { get; } = integrationFixture;
}