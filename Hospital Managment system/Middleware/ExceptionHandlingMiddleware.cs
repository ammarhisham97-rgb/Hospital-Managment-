using System.Net;
using System.Text.Json;
using Hospital_Managment_system.DTOs;
using Serilog;

namespace Hospital_Managment_system.Middleware;

/// <summary>
/// Global exception handling middleware for the application.
/// Catches all unhandled exceptions and returns consistent API responses.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = new ApiResponseDto<string>();

        switch (exception)
        {
            case ArgumentNullException argNullEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = "A required argument is missing";
                response.Error = argNullEx.Message;
                response.StatusCode = context.Response.StatusCode;
                Log.Warning("Argument null exception: {Message}", argNullEx.Message);
                break;

            case ArgumentException argEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = "Invalid argument provided";
                response.Error = argEx.Message;
                response.StatusCode = context.Response.StatusCode;
                Log.Warning("Argument exception: {Message}", argEx.Message);
                break;

            case InvalidOperationException invOpEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = "Invalid operation";
                response.Error = invOpEx.Message;
                response.StatusCode = context.Response.StatusCode;
                Log.Warning("Invalid operation exception: {Message}", invOpEx.Message);
                break;

            case KeyNotFoundException keyNotFoundEx:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Success = false;
                response.Message = "Resource not found";
                response.Error = keyNotFoundEx.Message;
                response.StatusCode = context.Response.StatusCode;
                Log.Warning("Resource not found: {Message}", keyNotFoundEx.Message);
                break;

            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Success = false;
                response.Message = "An unexpected error occurred";
                response.Error = exception.Message;
                response.StatusCode = context.Response.StatusCode;
                Log.Error(exception, "Unhandled exception: {Message}", exception.Message);
                break;
        }

        return context.Response.WriteAsJsonAsync(response);
    }
}
