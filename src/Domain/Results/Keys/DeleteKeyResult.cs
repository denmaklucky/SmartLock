using Domain.Dto;

namespace Domain.Results.Keys;

public class DeleteKeyResult : BaseResult
{
    public KeyDto Data { get; set; }
}