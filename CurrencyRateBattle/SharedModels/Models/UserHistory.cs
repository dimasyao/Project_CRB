using System.Text.Json.Serialization;

namespace SharedModels.Models;
public class UserHistory
{
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("prediction")]
    public decimal Prediction { get; set; }

    [JsonPropertyName("amountofbet")]
    public decimal AmountOfBet { get; set; }

    [JsonPropertyName("amountofwinning")]
    public decimal AmountOfWining { get; set; }

    [JsonPropertyName("haswon")]
    public bool HasWon { get; set; }
}
