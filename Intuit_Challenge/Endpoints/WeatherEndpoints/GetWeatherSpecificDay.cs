using AutoMapper;
using Intuit_Challenge.Endpoints.ClientEndpoints;
using Intuit_Challenge.Services;
using Microsoft.AspNetCore.Mvc;
using static Intuit_Challenge.Dtos.ClientDtos;
using Swashbuckle.AspNetCore.Annotations;

namespace Intuit_Challenge.Endpoints.WeatherEndpoints
{
    public class GetWeatherSpecificDay : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public GetWeatherSpecificDay(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("weather/getWeatherSpecificDay")]
        [SwaggerOperation(
            Summary = "Get Weather Specific Day",
            Description = "Get Weather Specific Day with location",
            OperationId = "getWeatherSpecificDay",
            Tags = new[] { "Weather" })
        ]
        public async Task<ActionResult<GetWeatherSpecificDayResponse>> GetAllClient([FromRoute] GetWeatherSpecificDayRequest req)
        {
            try
            {
                GetWeatherSpecificDayResponse response = new GetWeatherSpecificDayResponse
                {
                    WeatherDays = await _weatherService.GetWeatherSpecificDays(req.Location, req.DateTime)
                };
                return response;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        public class GetWeatherSpecificDayRequest
        {
            [FromQuery] public string Location { get; set; }
            [FromQuery] public DateTime DateTime { get; set; }
        }

        public class GetWeatherSpecificDayResponse
        {
            public List<WeatherDayValues> WeatherDays { get; set; }
        }
    }
}
