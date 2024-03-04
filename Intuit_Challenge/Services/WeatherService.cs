using Microsoft.AspNetCore.Authentication.BearerToken;
using Newtonsoft.Json;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace Intuit_Challenge.Services
{
    public interface IWeatherService
    {
        Task<List<WeatherDayValues>> GetWeather(string location, int? days = 6);
        Task<List<WeatherDayValues>> GetWeatherSpecificDays(string location, DateTime dateTime);
    }
    public class WeatherService : IWeatherService
    {
        private readonly IConfigurationService _configurationService;
        private string _key;
        private string _domain;
        public WeatherService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
            _key = _configurationService.GetSetting("VisualCrossing", "Key");
            _domain = "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/";
        }

        public async Task<List<WeatherDayValues>> GetWeather(string location, int? days = 6)
        {
            if (days < 0 || days > 14)
            {
                throw new Exception("Days out of range");
            }
            if (string.IsNullOrEmpty(location))
            {
                throw new Exception("Location null");
            }
            DateTime date1 = DateTime.UtcNow;
            DateTime date2 = DateTime.UtcNow.AddDays(days.Value);
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{_domain}{location}/{date1.ToString("yyyy-MM-dd'T'HH:mm:ss")}/{date2.ToString("yyyy-MM-dd'T'HH:mm:ss")}?key={_key}");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode(); // Throw an exception if error

            var body = await response.Content.ReadAsStringAsync(); // From the URL query code above 

            dynamic weather = JsonConvert.DeserializeObject(body);

            List<WeatherDayValues> dayValues = new List<WeatherDayValues>();

            foreach (var day in weather.days)
            {
                WeatherDayValues weatherDayValues = new WeatherDayValues
                {
                    weather_date = day.datetime,
                    weather_desc = day.description,
                    weather_tmax = day.tempmax,
                    weather_tmin = day.tempmin,
                    humidity = day.humidity,
                    precip = day.precip,
                    precipprob = day.precipprob
                };
                dayValues.Add(weatherDayValues);
            }

            return dayValues;
        }


        public async Task<List<WeatherDayValues>> GetWeatherSpecificDays(string location, DateTime dateTime)
        {
            if (string.IsNullOrEmpty(location))
            {
                throw new Exception("Location null");
            }
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{_domain}{location}/{dateTime.ToString("yyyy-MM-dd'T'HH:mm:ss")}?key={_key}");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode(); // Throw an exception if error

            var body = await response.Content.ReadAsStringAsync(); // From the URL query code above 

            dynamic weather = JsonConvert.DeserializeObject(body);

            List<WeatherDayValues> dayValues = new List<WeatherDayValues>();

            foreach (var day in weather.days)
            {
                WeatherDayValues weatherDayValues = new WeatherDayValues
                {
                    weather_date = day.datetime,
                    weather_desc = day.description,
                    weather_tmax = day.tempmax,
                    weather_tmin = day.tempmin,
                    humidity = day.humidity,
                    precip = day.precip,
                    precipprob = day.precipprob
                };
                dayValues.Add(weatherDayValues);
            }

            return dayValues;
        }

    }

    public class WeatherDayValues
    {
        public string weather_date { get; set; }
        public string weather_desc { get; set; }
        public string weather_tmax { get; set; }
        public string weather_tmin { get; set; }
        public string humidity { get; set; }
        public string precipprob { get; set; }
        public string precip { get; set; }
    }
}