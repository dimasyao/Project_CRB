using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SharedModels.Models;

public class Room
{
    [Key]
    [JsonPropertyName("roomid")]
    public string? RoomId { get; set; }
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
    [JsonPropertyName("bets")]
    public string? Bets { get; set; }
}
