using System.Text.Json.Serialization;

namespace template_API.Models
{
    public class WeatherModel
    {
        [JsonPropertyName("time")]
        public string[] Time { get; set; }
        [JsonPropertyName("temperature_2m_max")]
        public float[] Temperature_2m_max { get; set; }
        [JsonPropertyName("temperature_2m_min")]
        public float[] Temperature_2m_min { get; set; }
    }
}
