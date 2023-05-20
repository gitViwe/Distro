using System.Text.Json.Serialization;

namespace gitViwe.Shared.Model;

public class OverwatchMap
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("screenshot")]
    public string Screenshot { get; set; } = string.Empty;

    [JsonPropertyName("gamemodes")]
    public IEnumerable<string> Gamemodes { get; set; } = Enumerable.Empty<string>();

    [JsonPropertyName("location")]
    public string Location { get; set; } = string.Empty;

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; } = string.Empty;
}
