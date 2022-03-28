using Domain.Dto;

namespace Domain.Results.Keys;

public class ChangeUserForKeyResult : BaseResult
{
    public KeyDto Data { get; set; }
}