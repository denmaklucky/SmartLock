using Domain.Dto;

namespace Domain.Results.Keys;

public class ChangeLockForKeyResult : BaseResult
{
    public AccessLockDto Data { get; set; }
}