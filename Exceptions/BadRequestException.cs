namespace TestTask.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string detail) : base(detail)
    {
        StatusCode = StatusCodes.Status400BadRequest;
        Title = "Bad Request";
        Detail = detail;
    }

    public int StatusCode { get; }
    public string Title { get; }
    public string Detail { get; }
}