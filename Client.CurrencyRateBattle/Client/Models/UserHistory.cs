using System.Globalization;
using System.Text.Json.Serialization;

namespace Client.Models;

public class UserHistory
{
    private const string Format = "dddd, dd MMMM hh:mm tt";

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

    public override string ToString()
    {
        var ci = new CultureInfo("en-US");

        return $"{Date.ToString(Format, ci)} | {Currency} | Prediction: {Prediction} | Bet: {AmountOfBet} | Amount of winning: {AmountOfWining} ";
    }
}
