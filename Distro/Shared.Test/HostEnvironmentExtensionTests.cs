using System.Collections.Generic;
using gitViwe.Shared;
using gitViwe.Shared.Extension;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Shared.Test;

public class HostEnvironmentExtensionTests
{
    private class TestHostEnvironment : IHostEnvironment
    {
        public string ApplicationName { get; set; } = string.Empty;
        public IFileProvider ContentRootFileProvider { get; set; } = default!;
        public string ContentRootPath { get; set; } = string.Empty;
        public string EnvironmentName { get; set; } = string.Empty;
    }

    [Fact]
    public void IsDocker_ReturnsTrue_WhenEnvironmentIsDocker()
    {
        // Arrange
        var environment = new TestHostEnvironment { EnvironmentName = HubEnvironmentName.Docker };

        // Act
        var result = environment.IsDocker();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsDocker_ReturnsFalse_WhenEnvironmentIsNotDocker()
    {
        // Arrange
        var environment = new TestHostEnvironment { EnvironmentName = "Production" };

        // Act
        var result = environment.IsDocker();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsTest_ReturnsTrue_WhenEnvironmentIsTest()
    {
        // Arrange
        var environment = new TestHostEnvironment { EnvironmentName = HubEnvironmentName.Test };

        // Act
        var result = environment.IsTest();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsTest_ReturnsFalse_WhenEnvironmentIsNotTest()
    {
        // Arrange
        var environment = new TestHostEnvironment { EnvironmentName = "Production" };

        // Act
        var result = environment.IsTest();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsAny_ReturnsTrue_WhenEnvironmentMatchesOneOfTheGivenNames()
    {
        // Arrange
        var environment = new TestHostEnvironment { EnvironmentName = "Development" };
        var environmentNames = new List<string> { "Production", "Development", "Staging" };

        // Act
        var result = environment.IsAny(environmentNames);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsAny_ReturnsFalse_WhenEnvironmentDoesNotMatchAnyOfTheGivenNames()
    {
        // Arrange
        var environment = new TestHostEnvironment { EnvironmentName = "Testing" };
        var environmentNames = new List<string> { "Production", "Development", "Staging" };

        // Act
        var result = environment.IsAny(environmentNames);

        // Assert
        Assert.False(result);
    }
}
