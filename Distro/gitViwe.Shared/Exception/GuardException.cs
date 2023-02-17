namespace gitViwe.Shared.Exception;

/// <summary>
/// Throws an exception based on the condition being evaluated.
/// </summary>
internal static class GuardException
{
    private static bool IsSuccessStatusCode(int statusCode) => (statusCode >= 200) && (statusCode <= 299);
    /// <summary>
    /// Specifies conditions that must be false.
    /// </summary>
    internal static class Against
    {
        /// <summary>
        /// Throws an <seealso cref="ArgumentException"/> if the <paramref name="statusCode"/> value is a success status code.
        /// </summary>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <exception cref="ArgumentException"/>
        internal static void SuccessStatusCode(int statusCode)
        {
            if (IsSuccessStatusCode(statusCode))
            {
                throw new ArgumentException($"The success status code: [{statusCode}] is not allowed."); 
            }
        }

        /// <summary>
        /// Throws an <seealso cref="ArgumentException"/> if the <paramref name="statusCode"/> value is a failure status code.
        /// </summary>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <exception cref="ArgumentException"/>
        internal static void ErrorStatusCode(int statusCode)
        {
            if (IsSuccessStatusCode(statusCode) == false)
            {
                throw new ArgumentException($"The error status code: [{statusCode}] is not allowed.");
            }
        }
    }
}
