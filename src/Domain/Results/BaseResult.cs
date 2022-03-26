namespace Domain.Results;

public abstract class BaseResult
{
    public string ErrorCode { get; set; }
    public bool IsSuccess => string.IsNullOrWhiteSpace(ErrorCode);
}