namespace Domain.Results;

public class CreateLockResult : BaseResult
{
    public string[] ValidatorErrors { get; set; }
}