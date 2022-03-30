using Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domain.Dto;

public class OpeningHistoryDto
{
    public Guid LockId { get; set; }
    public DateTime OpenedOn { get; set; }
    public Guid AccessId { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public AccessTypeEnum AccessType { get; set; }
    public string UserName { get; set; }
}