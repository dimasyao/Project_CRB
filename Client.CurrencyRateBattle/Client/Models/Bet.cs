using System.Text.Json.Serialization;

namespace Client.Models;

public class Bet
{
    [JsonPropertyName("login")]
    public string? Login { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("sum")]
    public decimal Sum { get; set; }

    [JsonPropertyName("betvalue")]
    public decimal BetValue { get; set; }
}
