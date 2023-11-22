using System.Text;
using System.Text.Json;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Refit;
using Serilog;
using Shared.Exceptions;

namespace Shared.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly ILogger Logger = Log.ForContext<ExceptionMiddleware>();

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BFFException exception)
        {
            await BadRequest(context, exception, exception.Message);
        }
        catch (ValidationApiException validationException)
        {
            // handle validation here by using validationException.Content,
            // which is type of ProblemDetails according to RFC 7807

            // If the response contains additional properties on the problem details,
            // they will be added to the validationException.Content.Extensions collection.
        }
        catch (ApiException exception)
        {
            await BadRequest(context, exception, exception.Message);
        }
        catch (ValidationException ex)
        {
            await BadRequest(context, ex, ex.Message);
        }
        catch (Exception ex)
        {
            await InternalServerError(context, ex, ex.Message);
        }
    }

    private static async Task BadRequest(HttpContext context, BFFException exception, string contentType = "text/plain")
    {
        Logger.Warning(exception, "BFF Validation Exception");
        context.Response.Clear();
        context.Response.ContentType = contentType;
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsJsonAsync(exception.Message);
    }
    
    private static async Task BadRequest(HttpContext context, ApiException exception, string contentType = "application/json")
    {
        // TODO: Refit exception should be handled correctly.
        var content = JsonSerializer.Deserialize<List<ValidationFailure>>(exception.Content);
        Logger.Warning(exception, "Validation Exception");
        context.Response.Clear();
        context.Response.ContentType = contentType;
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsJsonAsync(exception.Content);
    }
    
    private static async Task BadRequest(HttpContext context, ValidationException exception, string contentType = "application/json")
    {
        Logger.Warning(exception, "Validation Exception");
        context.Response.Clear();
        context.Response.ContentType = contentType;
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsJsonAsync(exception.Errors);
    }
    
    private static async Task InternalServerError(HttpContext context, Exception exception, string contentType = "text/plain")
    {
        Logger.Error(exception, exception.Message);
        context.Response.Clear();
        context.Response.ContentType = contentType;
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync("An unexpected error occurred on the server", Encoding.UTF8);
    }
}