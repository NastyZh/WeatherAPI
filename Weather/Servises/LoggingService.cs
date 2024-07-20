namespace Weather;

public class LoggingService : ILoggingService
{
    private readonly ILoggerr _logger;

    public LoggingService(ILoggerr logger)
    {
       _logger=logger;
    }
    public void LogRequest(HttpContext context)
    {
        _logger.Log($"Request: {context.Request.Method} {context.Request.Path}");
        Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    }

    public void LogResponse(HttpContext context)
    {
        _logger.Log($"Response: {context.Response.StatusCode}");
    }
}