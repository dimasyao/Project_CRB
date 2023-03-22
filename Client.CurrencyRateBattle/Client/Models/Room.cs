using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Client.Models;

[Serializable]
public class Room
{
    private const string Format = "dddd, dd MMMM hh:mm tt";

    [Key]
    [JsonPropertyName("roomid")]
    public string? RoomId { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("bets")]
    public string? Bets { get; set; }

    public override string ToString()
    {
        var bets = new List<Bet>();
        var utcTimeOffcet = -(DateTime.UtcNow - DateTime.Now).Hours;

        if (!string.IsNullOrEmpty(Bets))
            bets = JsonSerializer.Deserialize<List<Bet>>(Bets!);

        var ci = new CultureInfo("en-US");

        return $"Currency: {Currency} | End time game: {Date.AddHours(utcTimeOffcet).ToString(Format, ci)} | Count of bets {bets!.Count}";
    }
}
