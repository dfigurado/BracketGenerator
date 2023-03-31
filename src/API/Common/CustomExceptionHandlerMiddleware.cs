using Application.Common.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace API.Common;

public class CustomExceptionHandlerMiddleware
{
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            await HandleExceptionAsync(context, e);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        var code = HttpStatusCode.InternalServerError;

        var result = string.Empty;

        switch (e)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(validationException.Errors);
                break;
            case NotFoundException _:
                code = HttpStatusCode.NotFound;
                break;

            case ArgumentException _:
                code = HttpStatusCode.BadRequest;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (string.IsNullOrEmpty(result))
        {
            result = JsonConvert.SerializeObject(new { error = e.Message });
        }

        return context.Response.WriteAsync(result);
    }
}

public static class CustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}