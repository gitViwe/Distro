using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using gitViwe.Shared.JsonWebToken;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Shared.Test;

public class JsonWebTokenTests
{
    private readonly IHost _host;
    
    public JsonWebTokenTests()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((_, config) =>
            {
                config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();
            })
            .ConfigureServices((_, services) =>
            {
                services.AddGitViweJsonWebToken();
            })
            .Build();
    }
    
    [Fact]
    public async Task CreateJsonWebToken_ShouldReturnValidToken()
    {
        // Arrange
        using IServiceScope serviceScope = _host.Services.CreateScope();
        var jsonWebToken = serviceScope.ServiceProvider.GetRequiredService<IJsonWebToken>();
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, "Test User"),
            new(ClaimTypes.Email, "test@example.com")
        };
        
        // Act
        var jwt = jsonWebToken.CreateJsonWebToken(claims, "https://localhost");
        var tokenClaimsPrincipal = await jsonWebToken.ValidateTokenAsync(jwt);
        
        // Assert
        Assert.NotEmpty(jwt);
        Assert.NotNull(tokenClaimsPrincipal);
        Assert.NotEmpty(tokenClaimsPrincipal.Claims);
        Assert.Contains(tokenClaimsPrincipal.Claims, c => c.Value == "Test User");
        Assert.Contains(tokenClaimsPrincipal.Claims, c => c.Value == "test@example.com");
    }
}