using Weather.Middleware;

namespace Weather.Extensions;

public static class WeatherMiddlewareExtensions
{
    public static  IApplicationBuilder UseWeatherMiddleware(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<WeatherMiddleware>();
    }
}