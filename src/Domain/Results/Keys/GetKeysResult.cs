using Domain.Dto;

namespace Domain.Results.Keys;

public class GetKeysResult : BaseResult
{
    public KeyDto[] Data { get; set; }
}