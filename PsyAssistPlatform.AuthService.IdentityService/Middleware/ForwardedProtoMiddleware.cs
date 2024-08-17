namespace PsyAssistPlatform.AuthService.IdentityService.Middleware;
public class ForwardedProtoMiddleware
{
    private readonly RequestDelegate _next;

    public ForwardedProtoMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine($"X-Forwarded-Proto added");
        context.Request.Headers["X-Forwarded-Proto"] = "https";
        await _next(context);
    }
}

public static class ForwardedProtoMiddlewareExtensions
{
    public static IApplicationBuilder UseForwardedProto(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ForwardedProtoMiddleware>();
    }
}
