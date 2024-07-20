using System.Text.Json;
using Weather;

public class WeatherMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWeatherService _weatherService;
    private readonly IAuthorizationService _authorizationService;
    private readonly ILogger<WeatherMiddleware> _logger;

    public WeatherMiddleware(RequestDelegate next, IWeatherService weatherService, IAuthorizationService authorizationService, ILogger<WeatherMiddleware> logger)
    {
        _next = next;
        _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
        _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path == "/" && context.Request.Method == "POST")
        {
            // Проверка заголовков авторизации
            if (!context.Request.Headers.TryGetValue("X-Weather-Token", out var token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Authorization header missing");
                return;
            }

            if (!_authorizationService.Authorize(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            // Проверка Content-Type
            if (context.Request.ContentType != "application/json")
            {
                context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                await context.Response.WriteAsync("Unsupported media type. Expected application/json.");
                return;
            }

            // Чтение и десериализация тела запроса
            using var reader = new StreamReader(context.Request.Body);
            var requestBody = await reader.ReadToEndAsync();
            
            _logger.LogInformation($"Request Body: {requestBody}");

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var cityRequest = JsonSerializer.Deserialize<CityRequest>(requestBody, options);

                if (cityRequest == null || string.IsNullOrEmpty(cityRequest.City))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Invalid request body format. City name is missing.");
                    return;
                }

                var weatherInfo = await _weatherService.GetWeatherAsync(cityRequest.City);
                var weatherInfoJson = JsonSerializer.Serialize(weatherInfo);

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(weatherInfoJson);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"JSON Deserialization Error: {ex.Message}");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid JSON format in request body.");
                return;
            }
        }
        else
        {
            await _next(context);
        }
    }
}
