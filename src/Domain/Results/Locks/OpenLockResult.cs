using Domain.Dto;

namespace Domain.Results.Locks;

public class OpenLockResult : BaseResult
{
    public OpenLockDto Data { get; set; }
    public string[] ValidatorErrors { get; set; }
}