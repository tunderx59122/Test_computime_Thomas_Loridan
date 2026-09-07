using System.Text.Json.Serialization;

namespace template_API.DTOs
{
    public class WeatherForecastFullDTO
    {
        [JsonPropertyName("results")]
        public required List<WeatherForecastDTO> Results { get; set; }
        [JsonPropertyName("generationtime_ms")]
        public float Generationtime_ms { get; set; }

    }
}
