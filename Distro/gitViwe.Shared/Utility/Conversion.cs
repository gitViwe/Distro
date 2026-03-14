using System.Reflection;
using System.Text.RegularExpressions;

namespace gitViwe.Shared;

/// <summary>
/// Provides common data conversion methods
/// </summary>
public static class Conversion
{
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
