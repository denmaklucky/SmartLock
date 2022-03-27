using Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domain.Dto;

public class LockDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public LockStateEnum State { get; set; }
    public bool IsDeleted { get; set; }
    public LockSettingDto Setting { get; set; }
    public KeyDto[] Keys { get; set; }
}

public class LockSettingDto
{
    [JsonConverter(typeof(StringEnumConverter))]
    public LockModeEnum Mode { get; set; }
    public TimeOnly? StartOpenTime { get; set; }
    public TimeOnly? EndOpenTime { get; set; }
}