namespace TestTask.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string description) : base(description)
    {
        StatusCode = StatusCodes.Status400BadRequest;
        Title = "Bad Request";
        Description = description;
    }

    public int StatusCode { get; }
    public string Title { get; }
    public string Description { get; }
}