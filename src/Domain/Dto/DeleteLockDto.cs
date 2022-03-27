namespace Domain.Dto;

public class DeleteLockDto
{
    public Guid LockId { get; set; }
    public bool IsDelete { get; set; }
}