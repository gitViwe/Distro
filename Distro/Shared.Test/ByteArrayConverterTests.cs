using System;
using Xunit;

namespace Shared.Test;

public class ByteArrayConverterTests
{
    [Fact]
    public void When_EmptyArray_ReturnsEmptyString()
    {
        // Arrange
        byte[] emptyArray = [];

        // Act
        string result = Convert.ToHexString(emptyArray);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void When_SingleByte_ReturnsCorrectHexString()
    {
        // Arrange
        byte[] singleByte = [0xAB];

        // Act
        string result = Convert.ToHexString(singleByte);

        // Assert
        Assert.Equal("AB", result);
    }

    [Fact]
    public void When_MultipleByte_ReturnsCorrectHexString()
    {
        // Arrange
        byte[] multipleBytes = [0xFB, 0x2F, 0x85, 0xC8, 0x85];

        // Act
        string result = Convert.ToHexString(multipleBytes);

        // Assert
        Assert.Equal("FB2F85C885", result);
    }

    [Fact]
    public void When_AllPossibleByteValues_ReturnsCorrectHexString()
    {
        // Arrange
        byte[] allBytes = new byte[256];
        for (int i = 0; i < 256; i++)
        {
            allBytes[i] = (byte)i;
        }

        // Act
        string result = Convert.ToHexString(allBytes);

        // Assert
        string expected = "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425262728292A2B2C2D2E2F303132333435363738393A3B3C3D3E3F404142434445464748494A4B4C4D4E4F505152535455565758595A5B5C5D5E5F606162636465666768696A6B6C6D6E6F707172737475767778797A7B7C7D7E7F808182838485868788898A8B8C8D8E8F909192939495969798999A9B9C9D9E9FA0A1A2A3A4A5A6A7A8A9AAABACADAEAFB0B1B2B3B4B5B6B7B8B9BABBBCBDBEBFC0C1C2C3C4C5C6C7C8C9CACBCCCDCECFD0D1D2D3D4D5D6D7D8D9DADBDCDDDEDFE0E1E2E3E4E5E6E7E8E9EAEBECEDEEEFF0F1F2F3F4F5F6F7F8F9FAFBFCFDFEFF";
        Assert.Equal(expected, result);
    }

    [Fact]
    public void When_NullArray_ThrowsArgumentNullException()
    {
        // Arrange
        byte[]? nullArray = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => Convert.ToHexString(nullArray!));
    }
}
