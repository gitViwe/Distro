namespace gitViwe.Shared.TimeBasedOneTimePassword;

/// <summary>
/// The hash algorithm.
/// </summary>
public enum TimeBasedOneTimePasswordAlgorithm
{
    /// <summary>
    /// SHA1
    /// </summary>
    SHA1 = 1,

    /// <summary>
    /// SHA256
    /// </summary>
    SHA256 = 2,

    /// <summary>
    /// SHA512
    /// </summary>
    SHA512 = 3
}
