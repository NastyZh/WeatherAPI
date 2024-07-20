using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Weather.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IWeatherService weatherService, ILogger<WeatherForecastController> logger)
        {
            _weatherService = weatherService;
            _logger = logger;
        }

        
        [HttpPost("GetWeather")]
        public async Task<IActionResult> GetWeather([FromBody] CityRequest cityRequest)
        {
            if (cityRequest == null || string.IsNullOrEmpty(cityRequest.City))
            {
                return BadRequest("Invalid request body format. City name is missing.");
            }

            var weatherInfo = await _weatherService.GetWeatherAsync(cityRequest.City);
            return Ok(JsonSerializer.Deserialize<object>(weatherInfo));
        }


       
    }
}