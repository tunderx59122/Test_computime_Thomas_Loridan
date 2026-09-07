using System.Text.Json.Serialization;

namespace template_API.DTOs
{
    public class WeatherForecastDTO
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("elevation")]
        public float Elevation { get; set; }

        [JsonPropertyName("feature_code")]
        public string FeatureCode { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("admin1_id")]
        public long Admin1Id { get; set; }

        [JsonPropertyName("admin2_id")]
        public long Admin2Id { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        [JsonPropertyName("population")]
        public long Population { get; set; }

        [JsonPropertyName("country_id")]
        public long CountryId { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("admin1")]
        public string Admin1 { get; set; }

        [JsonPropertyName("admin2")]
        public string Admin2 { get; set; }

    }
}
