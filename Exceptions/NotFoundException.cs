namespace TestTask.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
        StatusCode = StatusCodes.Status404NotFound;
        Title = "Запись не найдена";
        Detail = message;
    }

    public int StatusCode { get; }
    public string Title { get; }
    public string Detail { get; }
}