using Domain.Dto;

namespace Domain.Results.Locks;

public class UpdateLockResult : BaseResult
{
    public LockDto Data { get; set; }
}