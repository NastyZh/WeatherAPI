namespace Weather.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthorizationService _iAuthorizationService;

        public AuthorizationMiddleware(RequestDelegate next, IAuthorizationService iAuthorizationService)
        {
            _next = next;
            _iAuthorizationService = iAuthorizationService;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.TryGetValue("X-Weather-Token", out var token))
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await httpContext.Response.WriteAsync("Authorization header missing");
                return;
            }

            if (!_iAuthorizationService.Authorize(token))
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await httpContext.Response.WriteAsync("Unauthorized");
                return;
            }

            await _next(httpContext);
        }
    }
}
