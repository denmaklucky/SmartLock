using Domain.Dto;

namespace Domain.Results;

public class CreateLockResult : BaseResult
{
    public LockDto Data { get; set; }
    public string[] ValidatorErrors { get; set; }
}