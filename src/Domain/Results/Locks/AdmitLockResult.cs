using Domain.Dto;

namespace Domain.Results.Locks;

public class AdmitLockResult : BaseResult
{
    public AccessLockDto Data { get; set; }
}