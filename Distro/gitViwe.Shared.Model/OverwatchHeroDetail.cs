using System.Text.Json.Serialization;

namespace gitViwe.Shared.Model;

public class OverwatchHeroDetail
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("portrait")]
    public string Portrait { get; set; } = string.Empty;

    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;

    [JsonPropertyName("location")]
    public string Location { get; set; } = string.Empty;

    [JsonPropertyName("hitpoints")]
    public Hitpoints Hitpoints { get; set; } = new();

    [JsonPropertyName("abilities")]
    public IEnumerable<Ability> Abilities { get; set; } = Enumerable.Empty<Ability>();

    [JsonPropertyName("story")]
    public Story Story { get; set; } = new();
}

public class Ability
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("icon")]
    public string Icon { get; set; } = string.Empty;

    [JsonPropertyName("video")]
    public Video Video { get; set; } = new();
}

public class Chapter
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("picture")]
    public string Picture { get; set; } = string.Empty;
}

public class Hitpoints
{
    [JsonPropertyName("health")]
    public int Health { get; set; }

    [JsonPropertyName("armor")]
    public int Armor { get; set; }

    [JsonPropertyName("shields")]
    public int Shields { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }
}

public class Link
{
    [JsonPropertyName("mp4")]
    public string Mp4 { get; set; } = string.Empty;

    [JsonPropertyName("webm")]
    public string Webm { get; set; } = string.Empty;
}

public class Media
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("link")]
    public string Link { get; set; } = string.Empty;
}

public class Story
{
    [JsonPropertyName("summary")]
    public string Summary { get; set; } = string.Empty;

    [JsonPropertyName("media")]
    public Media Media { get; set; } = new Media();

    [JsonPropertyName("chapters")]
    public IEnumerable<Chapter> Chapters { get; set; } = Enumerable.Empty<Chapter>();
}

public class Video
{
    [JsonPropertyName("thumbnail")]
    public string Thumbnail { get; set; } = string.Empty;

    [JsonPropertyName("link")]
    public Link Link { get; set; } = new();
}
