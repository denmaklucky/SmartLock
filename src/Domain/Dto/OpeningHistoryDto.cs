namespace Domain.Dto;

public class OpeningHistoryDto
{
    public Guid LockId { get; set; }
    public DateTime OpenedOn { get; set; }
    public Guid AccessId { get; set; }
    public string UserName { get; set; }
}