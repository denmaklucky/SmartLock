using Domain.Dto;

namespace Domain.Results.Locks;

public class ForbidLockResult : BaseResult
{
    public AccessLockDto Data { get; set; }
}