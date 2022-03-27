using Model.Enums;

namespace Domain.Dto;

public class LockDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public LockStateEnum State { get; set; }
    public LockSettingDto Setting { get; set; }
}

public class LockSettingDto
{
    public LockModeEnum Mode { get; set; }
    public TimeOnly? StartTime { get; set; }
    public TimeOnly? EndTime { get; set; }
}