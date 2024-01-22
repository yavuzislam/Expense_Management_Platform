using System.Net;
using System.Text.Json;

namespace Expense_Management_Api.Middlewares;

public class HeartBeatMiddleware
{
    private readonly RequestDelegate _next;

    public HeartBeatMiddleware(RequestDelegate next)
    {
        _next = next;
    }


    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/hello"))
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            await context.Response.WriteAsync(JsonSerializer.Serialize("Hello from server"));
            return;
        }

        await _next.Invoke(context);
    }
}