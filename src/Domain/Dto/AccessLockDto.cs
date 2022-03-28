using Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domain.Dto;

public class AccessLockDto
{
    public Guid Id { get; set; }
    public Guid AccessId { get; set; }
    public Guid LockId { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public AccessTypeEnum Type { get; set; }
    public bool IsDeleted { get; set; }
}