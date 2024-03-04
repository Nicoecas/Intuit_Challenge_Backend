using Intuit_Challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Intuit_Challenge.Endpoints.WeatherEndpoints
{
    public class GetDaysWeather : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public GetDaysWeather(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("weather/getDaysWeather")]
        [SwaggerOperation(
            Summary = "Get Days Weather",
            Description = "Get Weather in location in futures days",
            OperationId = "getDaysWeather",
            Tags = new[] { "Weather" })
        ]
        public async Task<ActionResult<GetDaysWeatherResponse>> GetDaysWeathers([FromRoute] GetDaysWeatherRequest req)
        {
            try
            {
                GetDaysWeatherResponse response = new GetDaysWeatherResponse
                {
                    WeatherDays = await _weatherService.GetWeather(req.Location, req.Days)
                };
                return response;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        public class GetDaysWeatherRequest
        {
            [FromQuery] public string Location { get; set; }
            [FromQuery] public int? Days { get; set; }
        }

        public class GetDaysWeatherResponse
        {
            public List<WeatherDayValues> WeatherDays { get; set; }
        }
    }
}
