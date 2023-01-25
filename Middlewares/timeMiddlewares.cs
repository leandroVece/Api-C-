using Microsoft.AspNetCore.Http;
public class timeMiddleware
{
    readonly RequestDelegate next;

    public timeMiddleware(RequestDelegate nextRequest)
    {
        next = nextRequest;
    }

    public async Task Invoke(HttpContext content)
    {

        await next(content);
        if (content.Request.Query.Any(p => p.Key == "time"))
        {
            await content.Response.WriteAsync(DateTime.Now.ToLongDateString());
        }

    }

}

public static class TimeMiddlewareExtension
{
    public static IApplicationBuilder UseTimeMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<timeMiddleware>();
    }
}