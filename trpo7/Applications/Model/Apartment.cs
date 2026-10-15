using System.Text.Json.Serialization;

namespace trpo7.Application.Model;
public class Apartment
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;
    [JsonPropertyName("rooms")]
    public int Rooms { get; set; }
    [JsonPropertyName("area")]
    public double Area { get; set; }
    [JsonPropertyName("price")]
    public double Price { get; set; } 
    [JsonPropertyName("isAvailable")]
    public bool IsAvailable { get; set; }
}