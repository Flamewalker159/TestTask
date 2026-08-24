using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TestTask.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, title, detail) = exception switch
        {
            BadRequestException ex => (ex.StatusCode, ex.Title, ex.Detail),
            NotFoundException ex => (ex.StatusCode, ex.Title, ex.Detail),
            InternalServerException ex => (ex.StatusCode, ex.Title, ex.Detail),
            _ => (StatusCodes.Status500InternalServerError, "Неизвестная ошибка",
                $"Произошла необработанная ошибка: {exception.Message}")
        };

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/problem+json";

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path.ToString()
        };

        var jsonResponse = JsonSerializer.Serialize(problemDetails);
        await httpContext.Response.WriteAsync(jsonResponse, cancellationToken);

        return true;
    }
}