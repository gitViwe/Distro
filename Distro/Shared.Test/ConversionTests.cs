using gitViwe.Shared;
using System.Text.Json;
using Xunit;

namespace Shared.Test;

public class ConversionTests
{
    internal record TestObject(
        string Name,
        string Email,
        string Password,
        int Age);

    private record TestObjectObfuscateAttribute(
        string Name,
        [property: Obfuscate] string Email,
        [property: Obfuscate] string Password,
        int Age);

    [Fact]
    public void ToObfuscatedString_ShouldObfuscateProperties_WhenPropertyNamesGiven()
    {
        // Arrange
        var expected = new TestObject(
            Name: "John",
            Email: "john@example.com",
            Password: "secretpassword",
            Age: 30);

        string[] propertyNames = ["Email", "Password"];

        // Act
        string result = Conversion.ToObfuscatedString(expected, propertyNames);

        // Assert
        var deserialized = JsonSerializer.Deserialize<TestObject>(result);
        var deserializedObj = Assert.IsType<TestObject>(deserialized);
        Assert.Equal(expected.Name, deserializedObj.Name);
        Assert.Equal("*****", deserializedObj.Email);
        Assert.Equal("*****", deserializedObj.Password);
        Assert.Equal(expected.Age, deserializedObj.Age);
    }

    [Fact]
    public void ToObfuscatedString_ShouldObfuscateProperties_WhenPropertyAttributeUsed()
    {
        // Arrange
        var expected = new TestObjectObfuscateAttribute(
            Name: "John",
            Email: "john@example.com",
            Password: "secretpassword",
            Age: 30);

        // Act
        string result = Conversion.ToObfuscatedString(expected);

        // Assert
        var deserialized = JsonSerializer.Deserialize<TestObject>(result);
        var deserializedObj = Assert.IsType<TestObject>(deserialized);
        Assert.Equal(expected.Name, deserializedObj.Name);
        Assert.Equal("*****", deserializedObj.Email);
        Assert.Equal("*****", deserializedObj.Password);
        Assert.Equal(expected.Age, deserializedObj.Age);
    }

    [Fact]
    public void ToObfuscatedString_ShouldNotObfuscateProperties_WhenPropertyNamesNotGiven()
    {
        // Arrange
        var expected = new TestObject(
            Name: "John",
            Email: "john@example.com",
            Password: "secretpassword",
            Age: 30);

        // Act
        string result = Conversion.ToObfuscatedString(expected);

        // Assert
        var deserialized = JsonSerializer.Deserialize<TestObject>(result);
        var deserializedObj = Assert.IsType<TestObject>(deserialized);
        Assert.Equal(expected.Name, deserializedObj.Name);
        Assert.Equal(expected.Email, deserializedObj.Email);
        Assert.Equal(expected.Password, deserializedObj.Password);
        Assert.Equal(expected.Age, deserializedObj.Age);
    }
}
