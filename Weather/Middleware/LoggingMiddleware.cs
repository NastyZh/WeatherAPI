namespace Weather.Middleware;
public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggingService _iLoggingService;

    public LoggingMiddleware(RequestDelegate requestDelegate, ILoggingService iLoggingService)
    {
        _next = requestDelegate;
        _iLoggingService = iLoggingService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _iLoggingService.LogRequest(context);

        await _next(context);
        
        _iLoggingService.LogResponse(context);
    }
}

