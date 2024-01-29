namespace gitViwe.Shared.Utility;

/// <summary>
/// Provides a converter to formatting values
/// </summary>
public static class Formatter
{
    private static readonly string[] _suffixes = new string[] { "Bytes", "KB", "MB", "GB", "TB", "PB" };

    /// <summary>
    /// Formats the bytes size to the nearest larger format<br></br>
    /// 1024 Bytes  ---> 1 KB
    /// </summary>
    /// <param name="bytes">The size in bytes</param>
    /// <returns>A formatted byte size string</returns>
    public static string FormatSize(long bytes)
    {
        int counter = 0;
        decimal number = bytes;
        while (Math.Round(number / 1024) >= 1)
        {
            number /= 1024;
            counter++;
        }

        return string.Format("{0:#,0} {1}", number, _suffixes[counter]);
    }
}
