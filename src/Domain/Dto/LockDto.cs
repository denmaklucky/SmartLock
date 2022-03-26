using Model.Enums;

namespace Domain.Dto;

public class LockDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public LockStateEnum State { get; set; }
    public LockTypeEnum Type { get; set; }
    public LockSettingDto Setting { get; set; }
}

public class LockSettingDto
{
    
}