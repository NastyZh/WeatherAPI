using Weather.Middleware;

namespace Weather.Extensions;

public static class AuthorizationMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthorizationMiddleware(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<AuthorizationMiddleware>();
    }
}