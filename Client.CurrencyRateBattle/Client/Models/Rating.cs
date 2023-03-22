using System.Text.Json.Serialization;

namespace Client.Models;

[Serializable]
public class Rating
{
    [JsonPropertyName("login")]
    public string? Login { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("games")]
    public int Games { get; set; }
}
