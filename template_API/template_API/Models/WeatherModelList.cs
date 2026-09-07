using System.Text.Json.Serialization;

namespace template_API.Models
{
    public class WeatherModelList
    {
        [JsonPropertyName("daily")]
        public WeatherModel WeatherModels { get; set; }
    }
}
