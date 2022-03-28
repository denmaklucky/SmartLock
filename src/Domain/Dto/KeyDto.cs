using Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domain.Dto;

public class KeyDto
{
    public Guid Id { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public KeyTypeEnum Type { get; set; }
    public bool IsDeleted { get; set; }
    public Guid UserId { get; set; }
}