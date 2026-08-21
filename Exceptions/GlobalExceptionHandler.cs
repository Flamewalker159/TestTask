using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TestTask.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        var (statusCode, title, description) = exception switch
        {
            BadRequestException ex => (ex.StatusCode, ex.Title, ex.Description),
            _ => (StatusCodes.Status500InternalServerError, "Неизвестная ошибка", $"Произошла необработанная ошибка: {exception.Message}")
        };
        
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";
        
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = description,
            Instance = context.Request.Path.ToString()
        };
        
        var jsonResponse = JsonSerializer.Serialize(problemDetails);
        await context.Response.WriteAsync(jsonResponse, cancellationToken);

        return true;
    }
}