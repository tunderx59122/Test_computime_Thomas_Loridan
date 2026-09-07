using System.Globalization;
using System.Text.Json;
using template_API.DTOs;
using template_API.Models;

namespace template_API.Services
{
    public class WeatherService
    {
        HttpClient client = new HttpClient();
        public async Task<WeatherForecastDTO> GetCity(string city)
        {
            HttpResponseMessage response = await client.GetAsync($"https://geocoding-api.open-meteo.com/v1/search?name={city}&count=1&language=en&format=json");
            response.EnsureSuccessStatusCode();

            string apiResponse = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<WeatherForecastFullDTO>(apiResponse);

            WeatherForecastDTO weatherForcast = data.Results[0];
            return weatherForcast;
        }


        public async Task<WeatherModelList> GetHistoricalData(string city, string startDate, string endDate)
        {
            var cityInfo = await GetCity(city);
            var latitude = cityInfo.Latitude.ToString(CultureInfo.InvariantCulture);
            var longitude = cityInfo.Longitude.ToString(CultureInfo.InvariantCulture);

            HttpResponseMessage response = await client.GetAsync($"https://archive-api.open-meteo.com/v1/archive?latitude={latitude}&longitude={longitude}&start_date={startDate}&end_date={endDate}&daily=temperature_2m_max,temperature_2m_min&timezone_auto&temperature_unit=celsius");
            response.EnsureSuccessStatusCode();

            string apiResponse = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<WeatherModelList>(apiResponse);


            return data;
        }



        public async Task<float> GetTemperatureMean(string city, string startDate, string endDate)
        {
            var historicalData = await GetHistoricalData(city, startDate, endDate);

            float rangeSum = 0;
            var count_days = historicalData.WeatherModels.Time.Count();
            for (var i = 0; i < count_days; i++)
            {
                var maxTemp = historicalData.WeatherModels.Temperature_2m_max[i];
                var minTemp = historicalData.WeatherModels.Temperature_2m_min[i];
                rangeSum += maxTemp - minTemp;
            }

            return rangeSum / count_days;


        }


        public async Task<List<DailyTemperatureModel>> GetTemperatureRange(string city, string startDate, string endDate)
        {
            var historicalData = await GetHistoricalData(city, startDate, endDate);


            List<DailyTemperatureModel> list = new List<DailyTemperatureModel>();
            for (var i = 0; i < historicalData.WeatherModels.Time.Count(); i++)
            {
                var maxTemp = historicalData.WeatherModels.Temperature_2m_max[i];
                var minTemp = historicalData.WeatherModels.Temperature_2m_min[i];
                var range = maxTemp - minTemp;
                list.Add(new DailyTemperatureModel
                {
                    Date = historicalData.WeatherModels.Time[i],
                    MaxTemperature = maxTemp,
                    MinTemperature = minTemp,
                    Range = (float)Math.Round(range, 2)
                });

            }
            return list;
        }

    }
}
