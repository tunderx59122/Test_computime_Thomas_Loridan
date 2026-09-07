using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using template_API.DTOs;
using template_API.Models;
using template_API.Services;


namespace template_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherService _weatherService;
        static HttpClient client = new HttpClient();

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherService weatherService)
        {
            _logger = logger;
            this._weatherService = weatherService;
        }

        [HttpGet("city/{city}")]
        public async Task<WeatherForecastDTO> getCityInfo(string city)
        {
            var cityInfos = await _weatherService.GetCity(city);
            return cityInfos;
        
        }



        [HttpGet("historical-data/{city}&{startDate}&{endDate}")]
        public async Task<WeatherModelList> GetHistoricalData(string city, string startDate, string endDate)
        {
            var historicalData = await _weatherService.GetHistoricalData(city, startDate, endDate);
            return historicalData;
        }

        [HttpGet("historical-data/{city}&{startDate}&{endDate}/range")]
        public async Task<List<DailyTemperatureModel>> GetTemperatureRange(string city, string startDate, string endDate)
        {
            var ranges = await _weatherService.GetTemperatureRange(city, startDate, endDate);

            return ranges;
        }

        [HttpGet("historical-data/{city}&{startDate}&{endDate}/mean")]
        public async Task<float> GetTemperatureMean(string city, string startDate, string endDate)
        {
            var mean = await _weatherService.GetTemperatureMean(city, startDate, endDate);
            return mean;


        }

        [HttpGet("historical-data/{city}&{startDate}&{endDate}/standard_deviation")]
        public async Task<double> GetStandardDeviation(string city, string startDate, string endDate)
        {
            var historicalData = await _weatherService.GetHistoricalData(city, startDate, endDate);
            var ranges = await _weatherService.GetTemperatureRange(city, startDate, endDate);
            var mean = await _weatherService.GetTemperatureMean(city, startDate, endDate);
            float rangeSum = 0;
            var count_days = historicalData.WeatherModels.Time.Count();
            List<float> standardDeviationList = new List<float>();
            double sumRangeMinusMean = 0;
            for( var i = 0; i < count_days; i++)
            {
                sumRangeMinusMean += (Math.Pow((ranges[i].Range - mean), 2));
            }

            var standardDeviation = Math.Sqrt(sumRangeMinusMean /(count_days - 1));

            return standardDeviation;


        }

    }
}
