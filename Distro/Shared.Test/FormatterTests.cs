using gitViwe.Shared.Utility;
using Xunit;

namespace Shared.Test;

public class FormatterTests
{
    [Fact]
    public void FormatSize_ShouldReturnBytes_WhenInputIsLessThan1024()
    {
        // Arrange
        long bytes = 512;

        // Act
        string result = Formatter.FormatSize(bytes);

        // Assert
        Assert.Equal("512 Bytes", result);
    }

    [Fact]
    public void FormatSize_ShouldReturnKB_WhenInputIs1024()
    {
        // Arrange
        long bytes = 1024;

        // Act
        string result = Formatter.FormatSize(bytes);

        // Assert
        Assert.Equal("1 KB", result);
    }

    [Fact]
    public void FormatSize_ShouldReturnMB_WhenInputIs1048576()
    {
        // Arrange
        long bytes = 1048576; // 1024 * 1024

        // Act
        string result = Formatter.FormatSize(bytes);

        // Assert
        Assert.Equal("1 MB", result);
    }

    [Fact]
    public void FormatSize_ShouldReturnGB_WhenInputIs1073741824()
    {
        // Arrange
        long bytes = 1073741824; // 1024 * 1024 * 1024

        // Act
        string result = Formatter.FormatSize(bytes);

        // Assert
        Assert.Equal("1 GB", result);
    }
}
