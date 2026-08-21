namespace TestTask.Exceptions;

public class BadRequestExceptions : Exception
{
    public int StatusCode { get; }
    public string Title { get; }
    public string Description { get; }
    
    public BadRequestExceptions(string description) : base(description)
    {
        StatusCode = StatusCodes.Status400BadRequest;
        Title = "Bad Request";
        Description = description;
    }
}