using Domain.Dto;

namespace Domain.Results;

public class DeleteLockResult : BaseResult
{
    public DeleteLockDto Data { get; set; }
    public string[] ValidatorErrors { get; set; }
}