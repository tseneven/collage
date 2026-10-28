using System.Globalization;
using System.Text.Json.Serialization;
using trpo7.Application.Service;

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
    
    public ApartmentModel ToModel() => new()
    {
        Address = Address,
        Rooms = Rooms,
        Area = Area,
        Price = Price,
        IsAvailable = IsAvailable
    };
}

public class ApartmentDTO
{
    [JsonPropertyName("address")]
    public required string Address { get; set; }
    [JsonPropertyName("rooms")]
    public required string Rooms { get; set; }
    [JsonPropertyName("area")]
    public required string Area { get; set; }
    [JsonPropertyName("price")]
    public required string Price { get; set; } 
    [JsonPropertyName("isAvailable")]
    public required string IsAvailable { get; set; }
}