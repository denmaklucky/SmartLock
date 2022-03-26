namespace Domain.Exceptions;

public class LogicException : Exception
{
    public LogicException(string errorCode, string errorMessage)
        : base(errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }
    public string ErrorCode { get; }
    public string ErrorMessage { get; }
}