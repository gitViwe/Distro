namespace gitViwe.Shared.Abstraction;

/// <summary>
/// Defines the property required to validate the request origin host
/// </summary>
public interface IRequiresHost
{
    /// <summary>
    /// The Origin HTTP header value
    /// </summary>
    string Origin { get; init; }
}
