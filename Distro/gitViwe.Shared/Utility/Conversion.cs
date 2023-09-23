﻿namespace gitViwe.Shared;

/// <summary>
/// Provides common data conversion methods
/// </summary>
public static class Conversion
{
    /// <summary>
    /// This converts the 64 byte hash into the string hex representation of byte values 
    /// (shown by default as 2 hex characters per byte) that looks like 
    /// "FB-2F-85-C8-85-67-F3-C8-CE-9B-79-9C-7C-54-64-2D-0C-7B-41-F6...", each pair represents
    /// the byte value of 0-255. Removing the "-" separator creates a 128 character string 
    /// representation in hexadecimal
    /// </summary>
    /// <param name="value">The byte array to convert</param>
    public static string ByteArrayToString(byte[] value)
    {
        return BitConverter.ToString(value).Replace("-", "");
    }

    /// <summary>
    /// This converts the hex string to a byte array
    /// </summary>
    /// <param name="hex">The hexadecimal string to convert</param>
    /// <returns>A <see cref="byte"/> array value representing the string data</returns>
    public static byte[] StringToByteArray(string hex)
    {
        int NumberChars = hex.Length;
        byte[] bytes = new byte[NumberChars / 2];
        for (int i = 0; i < NumberChars; i += 2)
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        return bytes;
    }

    /// <summary>
    /// Gets the <see cref="DateTime"/> value from the time stamp
    /// </summary>
    /// <param name="unixTimeStamp">The Unix time stamp to convert</param>
    /// <returns>A <see cref="DateTime"/> value in <see cref="DateTimeKind.Utc"/> format</returns>
    public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        return DateTime.UnixEpoch.AddSeconds(unixTimeStamp).ToUniversalTime();
    }

    /// <summary>
    /// Gets the UnixTimeStamp as <see cref="Int64"/> value
    /// </summary>
    /// <param name="dateTime">The date value in <seealso cref="DateTimeKind.Utc"/> format</param>
    /// <returns>A <see cref="Int64"/> value representing the UnixTimeStamp</returns>
    public static long DateTimeToUnixTimeStamp(DateTime dateTime)
    {
        return (long)dateTime.Subtract(DateTime.UnixEpoch).TotalSeconds;
    }

    /// <summary>
    /// This converts the base 64 string to a byte array
    /// </summary>
    /// <param name="payload">The base 64 string without padding</param>
    /// <returns>A <see cref="byte"/> array value representing the string data</returns>
    public static byte[] ParseBase64WithoutPadding(string payload)
    {
        payload = payload.Trim().Replace('-', '+').Replace('_', '/');
        var base64 = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
        return Convert.FromBase64String(base64);
    }
}
