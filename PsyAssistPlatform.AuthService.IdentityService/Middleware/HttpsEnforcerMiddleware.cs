using System.Text;

namespace PsyAssistPlatform.AuthService.IdentityService.Middleware;
public class HttpsEnforcerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _internalHost;
    private readonly string _publicHost;

    public HttpsEnforcerMiddleware(RequestDelegate next, string internalHost, string publicHost)
    {
        _next = next;
        _internalHost = internalHost;
        _publicHost = publicHost;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        context.Response.Body = originalBodyStream;

        if (context.Request.Path.StartsWithSegments("/.well-known/openid-configuration"))
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            var json = await new StreamReader(responseBody).ReadToEndAsync();

            json = json.Replace(_internalHost, _publicHost);

            var modifiedBytes = Encoding.UTF8.GetBytes(json);
            context.Response.ContentLength = modifiedBytes.Length;
            await context.Response.Body.WriteAsync(modifiedBytes, 0, modifiedBytes.Length);
        }
        else
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}

public static class HttpsEnforcerMiddlewareExtensions
{
    public static IApplicationBuilder UseHttpsEnforcer(this IApplicationBuilder builder, string internalHost, string publicHost)
    {
        return builder.UseMiddleware<HttpsEnforcerMiddleware>(internalHost, publicHost);
    }
}
