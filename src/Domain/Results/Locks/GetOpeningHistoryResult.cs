using Domain.Dto;

namespace Domain.Results.Locks;

public class GetOpeningHistoryResult : BaseResult
{
    public OpeningHistoryDto[] Data { get; set; }
}