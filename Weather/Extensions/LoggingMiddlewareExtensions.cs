using Weather.Middleware;

namespace Weather.Extensions;

public static class LoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<LoggingMiddleware>();
    }
}