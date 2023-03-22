using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Client.Models;
public class User
{
    [Key]
    [JsonPropertyName("login")]
    public string? Login { get; set; }
    [JsonPropertyName("password")]
    public string? Password { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("balance")]
    public decimal Balance { get; set; }
    [JsonPropertyName("totalgames")]
    public int TotalGames { get; set; }
    [JsonPropertyName("victories")]
    public int Victories { get; set; }
    [JsonPropertyName("history")]
    public string? History { get; set; }
}
