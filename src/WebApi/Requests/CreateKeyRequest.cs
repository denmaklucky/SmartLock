using System.ComponentModel.DataAnnotations;
using Model.Enums;

namespace WebApi.Requests;

public record CreateKeyRequest([Required]string LockId, [Required]string UserId, [Required]KeyTypeEnum Type);