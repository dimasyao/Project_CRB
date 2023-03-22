using System.Text.Json.Serialization;

namespace RoomManager.Objects;

public class Currency
{
    [JsonPropertyName("r030")]
    public int R030 { get; set; }

    [JsonPropertyName("txt")]
    public string? TXT { get; set; }

    [JsonPropertyName("rate")]
    public decimal Rate { get; set; }

    [JsonPropertyName("cc")]
    public string? CC { get; set; }

    [JsonPropertyName("exchangedate")]
    public string? Exchangedate { get; set; }
}
