using Domain.Dto;

namespace Domain.Results.Locks;

public class CreateLockResult : BaseResult
{
    public LockDto Data { get; set; }
}