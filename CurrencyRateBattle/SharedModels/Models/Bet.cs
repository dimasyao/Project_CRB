using System.Text.Json.Serialization;

namespace SharedModels.Models;

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

    public override string ToString()
    {
        return $"Bet| Login: {Login} | Curr: {Currency} | Summ: {Sum}  | BetValue: {BetValue} |";
    }
}
