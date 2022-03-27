using Domain.Dto;

namespace Domain.Results.Keys;

public class CreateKeyResult : BaseResult
{
    public KeyDto Data { get; set; }
}