namespace Domain.Results;

public abstract class BaseResult
{
    public string ErrorType { get; set; }
    public bool IsSuccess => string.IsNullOrWhiteSpace(ErrorType);
}