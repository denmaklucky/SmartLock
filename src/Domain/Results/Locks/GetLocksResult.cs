using Domain.Dto;

namespace Domain.Results.Locks;

public class GetLocksResult : BaseResult
{
    public LockDto[] Data { get; set; }
}