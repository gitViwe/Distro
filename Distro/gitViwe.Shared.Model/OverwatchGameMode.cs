using System.Text.Json.Serialization;

namespace gitViwe.Shared.Model;

public class OverwatchGameMode
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("icon")]
    public string Icon { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("screenshot")]
    public string Screenshot { get; set; } = string.Empty;
}
