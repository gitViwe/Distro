using System.Text;

namespace gitViwe.Shared;

/// <summary>
/// Extends on <seealso cref="IDefaultProblemDetails"/> to add <seealso cref="Errors"/>
/// </summary>
public interface IValidationProblemDetails : IDefaultProblemDetails
{
    /// <summary>
    /// Gets the validation errors associated with this instance of HttpValidationProblemDetails
    /// </summary>
    IDictionary<string, string[]> Errors { get; set; }

    /// <summary>
    /// Format the <seealso cref="Errors"/> dictionary into a readable string
    /// </summary>
    /// <returns>A string of the <seealso cref="Errors"/> values</returns>
    public string? ErrorsToDebugString()
    {
        return Errors is null
            ? string.Empty
            : '{' + string.Join(',', Errors.Select(kv => kv.Key + '=' + string.Join('|', kv.Value)).ToArray()) + '}';
    }

    /// <summary>
    /// A string that represents the current <seealso cref="IValidationProblemDetails"/>.
    /// </summary>
    /// <returns>A string that represents the <seealso cref="IValidationProblemDetails"/>.</returns>
    public new string? ToString() =>
        new StringBuilder()
            .AppendLine($"TraceId   : {TraceId}")
            .AppendLine($"Type      : {Type}")
            .AppendLine($"Title     : {Title}")
            .AppendLine($"Status    : {Status}")
            .AppendLine($"Detail    : {Detail}")
            .AppendLine($"Instance  : {Instance}")
            .AppendLine($"Errors    : {ErrorsToDebugString()}")
            .AppendLine($"Extensions: {ExtensionsToDebugString()}")
            .ToString();
}
