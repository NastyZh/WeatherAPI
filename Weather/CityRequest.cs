using System.Text.Json.Serialization;

namespace Weather;

public class CityRequest
{
    [JsonPropertyName("city")]
    public string City { get; set; }
} 
