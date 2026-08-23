namespace TestTask.Exceptions;

public class InternalServerException : Exception
{
    public int StatusCode { get; }
    public string Title { get; }
    public string Detail { get; }

    public InternalServerException(string message, Exception? innerException = null) : base(message, innerException)
    {
        StatusCode = StatusCodes.Status500InternalServerError;
        Title = "Внутренняя ошибка сервера";
        Detail = message;
    }
}