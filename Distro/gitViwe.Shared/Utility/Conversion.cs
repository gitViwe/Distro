using System.Reflection;
using System.Text.RegularExpressions;

namespace gitViwe.Shared;

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
    [Obsolete("This method will be removed in future versions. Use Convert.ToHexString(byte) instead.")]
    public static string ByteArrayToHexadecimalString(byte[] value)
        => BitConverter.ToString(value).Replace("-", string.Empty);

    /// <summary>
    /// This converts the hex string to a byte array
    /// </summary>
    /// <param name="hex">The hexadecimal string to convert</param>
    /// <returns>A <see cref="byte"/> array value representing the string data</returns>
    [Obsolete("This method will be removed in future versions. Use Convert.FromHexString(chars) instead.")]
    public static byte[] HexadecimalStringToByteArray(string hex)
    {
        int numberChars = hex.Length;
        byte[] bytes = new byte[numberChars / 2];
        for (int i = 0; i < numberChars; i += 2)
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        return bytes;
    }

    /// <summary>
    /// Gets the <see cref="DateTime"/> value from the time stamp
    /// </summary>
    /// <param name="unixTimeStamp">The Unix time stamp to convert</param>
    /// <returns>A <see cref="DateTime"/> value in <see cref="DateTimeKind.Utc"/> format</returns>
    public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        => DateTime.UnixEpoch.AddSeconds(unixTimeStamp).ToUniversalTime();

    /// <summary>
    /// Gets the UnixTimeStamp as <see cref="Int64"/> value
    /// </summary>
    /// <param name="dateTime">The date value in <seealso cref="DateTimeKind.Utc"/> format</param>
    /// <returns>A <see cref="Int64"/> value representing the UnixTimeStamp</returns>
    public static long DateTimeToUnixTimeStamp(DateTime dateTime)
        => (long)dateTime.Subtract(DateTime.UnixEpoch).TotalSeconds;

    /// <summary>
    /// This converts the base 64 string to a byte array
    /// </summary>
    /// <param name="payload">The base 64 string without padding</param>
    /// <returns>A <see cref="byte"/> array value representing the string data</returns>
    [Obsolete("This method will be removed in future versions. Use Convert.FromBase64String(value) instead.")]
    public static byte[] ParseBase64WithoutPadding(string payload)
    {
        payload = payload.Trim().Replace('-', '+').Replace('_', '/');
        var base64 = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
        return Convert.FromBase64String(base64);
    }
    
    /// <summary>
    /// Converts the provided value into a JSON <see cref="string"/> where the property values that have the <see cref="ObfuscateAttribute"/> are hidden.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
    /// <param name="request">The value to convert into a JSON <see cref="string"/>.</param>
    /// <returns>A JSON <see cref="string"/> representation of the value.</returns>
    public static string ToObfuscatedString<TValue>(TValue request)
        => ToObfuscatedString(request, []);

    /// <summary>
    /// Converts the provided value into a JSON <see cref="string"/> where the property values defined in <paramref name="propertyNames"/> and have the <see cref="ObfuscateAttribute"/> are hidden.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
    /// <param name="request">The value to convert into a JSON <see cref="string"/>.</param>
    /// <param name="propertyNames">The property names from <typeparamref name="TValue"/> to obfuscate.</param>
    /// <returns>A JSON <see cref="string"/> representation of the value.</returns>
    public static string ToObfuscatedString<TValue>(TValue request, IEnumerable<string> propertyNames)
    {
        string text = System.Text.Json.JsonSerializer.Serialize(request);
        var resolvedNames = ResolvePropertyNames().ToArray();

        if (resolvedNames.Length == 0)
        {
            return text;
        }

        // Join the property names with "|" to create the regex pattern
        string pattern = $"(\"({string.Join('|', resolvedNames)})\":\\s*)\"[^\\\"]*\"";
        const string replacement = "$1\"*****\"";

        return Regex.Replace(text, pattern, replacement);

        IEnumerable<string> ResolvePropertyNames()
        {
            var additionalPropertyNames = typeof(TValue).GetProperties()
                .Where(x => x.GetCustomAttribute<ObfuscateAttribute>() is not null)
                .Select(x => x.Name)
                .ToHashSet();

            // merge propertyNames into additionalPropertyNames
            foreach (string name in propertyNames)
            {
                additionalPropertyNames.Add(name);
            }

            return additionalPropertyNames;
        }
    }
}
