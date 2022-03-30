using System.ComponentModel.DataAnnotations;
using Model.Enums;

namespace WebApi.Requests;

public record OpenLockRequest(string KeyId, [Required]AccessTypeEnum AccessType);