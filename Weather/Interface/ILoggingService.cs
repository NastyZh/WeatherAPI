namespace Weather;

public interface ILoggingService
{
    void LogRequest(HttpContext httpContext);
    void LogResponse(HttpContext httpContext);

}
